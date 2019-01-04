using LC.Oauth2.DbContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LC.Oauth2.Shared;
using LC.Oauth2.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;

namespace LC.Oauth2.AdminServer
{
    /// <summary>
    /// TokenProvider 中间件
    /// 参考资料 
    /// https://www.cnblogs.com/hzzxq/p/7373287.html
    /// https://www.jianshu.com/p/294ea94f0087?utm_source=oschina-app
    /// </summary>
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly TokenProviderOptions _TokenOptions;
        private IServiceProvider _ServiceProvider;
        private Oauth2DbContext _DbCotext;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options,
            IServiceProvider serviceProvider, IAuthenticationSchemeProvider schemes)
        {
            _Next = next;
            _TokenOptions = options.Value;
            _ServiceProvider = serviceProvider;
            Schemes = schemes;
        }

        public IAuthenticationSchemeProvider Schemes { get; set; }

        /// <summary>
        /// 执行中间件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            using (var serviceScope = _ServiceProvider.CreateScope()) //手动高亮
            {
                _DbCotext = serviceScope.ServiceProvider.GetService<Oauth2DbContext>();

                context.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
                {
                    OriginalPath = context.Request.Path,
                    OriginalPathBase = context.Request.PathBase
                });


                IAuthenticationHandlerProvider handlers = context.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (AuthenticationScheme scheme in await Schemes.GetRequestHandlerSchemesAsync())
                {
                    var handler = await handlers.GetHandlerAsync(context, scheme.Name) as IAuthenticationRequestHandler;
                    if (handler != null && await handler.HandleRequestAsync())
                    {
                        return;
                    }
                }

                AuthenticationScheme defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    AuthenticateResult result = await context.AuthenticateAsync(defaultAuthenticate.Name);
                    if (result?.Principal != null)
                    {
                        context.User = result.Principal;
                    }
                }

                if (!context.Request.Path.Equals(_TokenOptions.Path, StringComparison.OrdinalIgnoreCase))
                {
                    await _Next(context);
                    return;
                }

                // Request must be POST with Content-Type: application/x-www-form-urlencoded
                if (!context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase)
                   || !context.Request.HasFormContentType)
                {
                    await ReturnBadRequest(context);
                    return;
                }

                //返回Token
                await GenerateAuthorizedResult(context);
            }
        }

        /// <summary>
        /// 返回错误结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="msg">错误消息</param>
        /// <returns></returns>
        private async Task ReturnBadRequest(HttpContext context, string msg = "认证失败")
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Code = -1,
                Message = msg
            }));
        }


        /// <summary>
        /// 验证结果并得到token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task GenerateAuthorizedResult(HttpContext context)
        {
            var account = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            if (string.IsNullOrEmpty(account))
            {
                await ReturnBadRequest(context, "账号不能为空");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                await ReturnBadRequest(context, "密码不能为空");
                return;
            }

            //验证用户
            var user = await FindUser(account, password);
            if (user == null)
            {
                await ReturnBadRequest(context, "该用户不存在，请检查账号和密码");
                return;
            }

            if (user.IsLock)
            {
                await ReturnBadRequest(context, "该用户已被禁用，请联系管理人员");
                return;
            }

            //身份信息
            var userCliaimsIndentity = await CreateUserClaims(context, user);

            //返回Token信息
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(BuildJwt(user, userCliaimsIndentity));
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private Task<SysUser> FindUser(string account, string password)
        {
            //密码加密
            var enctyptPwd = password.Encrypt();

            return _DbCotext.SysUsers.FirstOrDefaultAsync(m => m.Account == account && m.Password == enctyptPwd);
        }

        /// <summary>
        /// 获取用户身份信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private Task<ClaimsIdentity> CreateUserClaims(HttpContext context, SysUser user)
        {
            //TODO 添加身份信息
            return Task.FromResult(new ClaimsIdentity(new GenericIdentity(user.Account, "Token"), new Claim[] { }));
        }

        /// <summary>
        /// 创建 JSON WEB TOKEN
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="ClaimsIdentity">用户身份信息</param>
        /// <returns></returns>
        private string BuildJwt(SysUser user, ClaimsIdentity claimsIdentity)
        {
            DateTime now = DateTime.UtcNow;

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Account),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(),
                            ClaimValueTypes.Integer64),
                //TODO 配置信息

                //账号
                new Claim(ClaimTypes.Name,user.Account),
                //昵称
                new Claim(LCClaimTypes.NickName,user.NickName),
                //用户ID
                new Claim(LCClaimTypes.UserId,user.Id.ToString())
        };

            //管理员
            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            JwtSecurityToken jwt = new JwtSecurityToken(
             issuer: _TokenOptions.Issuer,
             audience: _TokenOptions.Audience,
             claims: claims,
             notBefore: now,
             expires: now.Add(_TokenOptions.Expiration),
             signingCredentials: _TokenOptions.SigningCredentials
             );
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                code = 0,
                access_token = encodedJwt,
                expires_in = (int)_TokenOptions.Expiration.TotalSeconds,
                token_type = "Bearer"
            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
        }

    }
}

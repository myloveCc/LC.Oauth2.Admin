using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using LC.Oauth2.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LC.Oauth2.AdminServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly Oauth2DbContext _DbContext;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<AccountController> _Logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public AccountController(Oauth2DbContext dbContext, ILogger<AccountController> logger)
        {
            _DbContext = dbContext;
            _Logger = logger;
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        [Authorize]
        public IActionResult Post()
        {
            var jtiClaim = User.Claims.FirstOrDefault(m => m.Type == JwtRegisteredClaimNames.Jti);

            //TODO ，根据JtiClaim值，设置Token过期

            return new JsonResult(new { code = 0, msg = "" });
        }
    }
}
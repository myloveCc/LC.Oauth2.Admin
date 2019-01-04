using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LC.Oauth2.AdminServer.Controllers.Base;
using LC.Oauth2.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LC.Oauth2.AdminServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : LcControllerBase
    {
        private readonly Oauth2DbContext _DbContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public UserController(Oauth2DbContext dbContext)
        {
            _DbContext = dbContext;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Get()
        {
            var userEntity = _DbContext.SysUsers.FirstOrDefault(m => m.Id == UserId);
            return new JsonResult(new
            {
                name = userEntity.NickName,
                user_id = userEntity.Id,
                access = new string[] { "super_admin", "admin" },
                avator = "https://file.iviewui.com/dist/a0e88e83800f138b94d2414621bd9704.png"
            });
        }
    }
}
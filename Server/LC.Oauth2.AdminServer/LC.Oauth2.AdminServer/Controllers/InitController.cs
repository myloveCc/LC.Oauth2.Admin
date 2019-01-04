using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LC.Oauth2.Entities;
using LC.Oauth2.Shared;
using LC.Oauth2.DbContext;
using Microsoft.AspNetCore.Authorization;

namespace LC.Oauth2.AdminServer.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class InitController : ControllerBase
    {
        private readonly Oauth2DbContext _DbContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public InitController(Oauth2DbContext dbContext)
        {
            _DbContext = dbContext;
        }

        /// <summary>
        /// 管理员初始化
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get()
        {
            var model = new SysUser()
            {
                Account = "admin",
                Password = "123456".Encrypt(),
                NickName = "超级管理员",
                IsAdmin = true
            };

            if (!_DbContext.SysUsers.Any(m => m.Account == model.Account))
            {
                _DbContext.Add(model);
                _DbContext.SaveChanges();

                return new JsonResult(new { code = 0, msg = $"账号已初始化成功,账号:admin,密码:123456。请尽快修改密码！" });
            }
            else
            {
                return new JsonResult(new { code = 1, msg = "管理员账号已存储，无需再次初始化。" });
            }
        }


    }
}

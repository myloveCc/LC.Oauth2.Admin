using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.Oauth2.AdminServer.ViewModel;
using LC.Oauth2.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LC.Oauth2.AdminServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<LogController> _Logger;

        /// <summary>
        /// 数据库对象
        /// </summary>
        private readonly Oauth2DbContext _DbContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public LogController(Oauth2DbContext dbContext, ILogger<LogController> logger)
        {
            _DbContext = dbContext;
            _Logger = logger;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log"></param>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] ErrorLog log)
        {
            //记录日志到本地
            _Logger.LogError("前端错误日志：{0}", JsonConvert.SerializeObject(log));

            //TODO 将日志记录到数据库
            return new JsonResult(new { code = 0, msg = "保存成功" });
        }
    }
}

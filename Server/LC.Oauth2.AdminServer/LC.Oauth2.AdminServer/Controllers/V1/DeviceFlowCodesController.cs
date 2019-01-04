using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.Oauth2.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LC.Oauth2.AdminServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DeviceFlowCodesController : ControllerBase
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly Oauth2DbContext _DbContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public DeviceFlowCodesController(Oauth2DbContext dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.Oauth2.AdminServer.ViewModel
{
    /// <summary>
    /// 错误日志
    /// </summary>
    public class ErrorLog
    {
        /// <summary>
        /// 请求返回码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 请求类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Mes { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// JWT Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public long Time { get; set; }
    }
}

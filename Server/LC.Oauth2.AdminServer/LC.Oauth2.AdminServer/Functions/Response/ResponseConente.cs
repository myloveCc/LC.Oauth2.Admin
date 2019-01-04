using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.Oauth2.AdminServer.Functions.Response
{
    /// <summary>
    /// Response content
    /// </summary>
    public class ResponseContent
    {
        /// <summary>
        /// Reqeust parameter has any error response
        /// </summary>
        public static ResponseContent ParameterErrorResponse = new ResponseContent(ResponseCode.ParameterError);

        /// <summary>
        /// Resource has existed resposne
        /// </summary>
        public static ResponseContent RsourceExistResponse = new ResponseContent(ResponseCode.ResourceExist);

        /// <summary>
        /// Resource not found resposne
        /// </summary>
        public static ResponseContent RsourceNotFoundResponse = new ResponseContent(ResponseCode.ResourceNotFound);

        /// <summary>
        /// 回复码
        /// </summary>
        public int Code { get; private set; } = 0;

        /// <summary>
        /// 附加消息
        /// </summary>
        public string Msg { get; set; } = "";

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ResponseContent()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">回复码，默认为0</param>
        /// <param name="msg">附加消息，默认为空</param>
        public ResponseContent(int code, string msg = "")
        {
            Code = code;
            Msg = msg;
        }
    }

    /// <summary>
    /// 回复内容，附加数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseContent<T> : ResponseContent where T : class
    {
        /// <summary>
        /// 附加数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">附加数据</param>
        public ResponseContent(T data = null)
        {
            Data = data;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">回复码</param>
        /// <param name="data">附加数据</param>
        public ResponseContent(int code, T data = null) : base(code)
        {
            Data = data;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">回复码</param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public ResponseContent(int code, string msg, T data = null) : base(code, msg)
        {
            Data = data;
        }
    }
}

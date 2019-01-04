using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.Oauth2.AdminServer.Functions.Response
{
    public class ResponseProvider
    {
        /// <summary>
        /// Ok response
        /// </summary>
        /// <param name="msg">msg</param>
        /// <returns></returns>
        public static Task<JsonResult> OkResult(string msg = "")
        {
            return Task.Factory.StartNew(() =>
            {
                ResponseContent response = new ResponseContent();

                if (msg != null)
                {
                    response.Msg = msg;
                }

                return new JsonResult(response);
            });
        }

        /// <summary>
        /// Ok response with data
        /// </summary>
        /// <param name="msg"> error msg</param>
        /// <returns></returns>
        public static Task<JsonResult> OkResult<T>(T data, string msg = "") where T : class
        {
            return Task.Factory.StartNew(() =>
            {
                ResponseContent<T> response = new ResponseContent<T>(data);

                if (msg != null)
                {
                    response.Msg = msg;
                }

                return new JsonResult(response);
            });
        }



        /// <summary>
        /// Parameter error response
        /// </summary>
        /// <param name="msg"> error msg</param>
        /// <returns></returns>
        public static Task<JsonResult> ParameterErrorResponse(string msg = "Request paramter is error")
        {
            return Task.Factory.StartNew(() =>
            {
                ResponseContent response = ResponseContent.ParameterErrorResponse;

                if (msg != null)
                {
                    response.Msg = msg;
                }

                return new JsonResult(response);
            });
        }

        /// <summary>
        /// Resource exist response
        /// </summary>
        /// <param name="msg">error msg</param>
        /// <returns></returns>
        public static Task<JsonResult> ResourceExistResponse(string msg = "Resource is exist,please change other resource")
        {
            return Task.Factory.StartNew(() =>
            {
                ResponseContent response = ResponseContent.RsourceExistResponse;

                if (msg != null)
                {
                    response.Msg = msg;
                }

                return new JsonResult(response);
            });
        }


        /// <summary>
        /// Resource not found response
        /// </summary>
        /// <param name="msg">error msg</param>
        /// <returns></returns>
        public static Task<JsonResult> ResourceNotFoundResponse(string msg = "Resource not found")
        {
            return Task.Factory.StartNew(() =>
            {
                ResponseContent response = ResponseContent.RsourceNotFoundResponse;

                if (msg != null)
                {
                    response.Msg = msg;
                }

                return new JsonResult(response);
            });
        }
    }
}

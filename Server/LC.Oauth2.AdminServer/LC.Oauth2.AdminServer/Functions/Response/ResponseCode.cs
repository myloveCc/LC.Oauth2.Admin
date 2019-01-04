using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.Oauth2.AdminServer.Functions.Response
{
    /// <summary>
    /// Response code
    /// </summary>
    public static class ResponseCode
    {
        /// <summary>
        /// Parameter error code
        /// </summary>
        public static int ParameterError { get; private set; } = 10001;

        /// <summary>
        /// Resource exist code
        /// </summary>
        public static int ResourceExist { get; private set; } = 10002;

        /// <summary>
        /// Resource not found
        /// </summary>
        public static int ResourceNotFound{ get; private set; } = 10003;
    }
}

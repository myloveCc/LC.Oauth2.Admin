using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LC.Oauth2.AdminServer.Controllers.Base
{
    public class LcControllerBase : ControllerBase
    {

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual int UserId
        {
            get
            {
                string userIdValue = User.Identities.First(m => m.IsAuthenticated).FindFirst(LCClaimTypes.UserId).Value;
                int.TryParse(userIdValue, out int userId);
                return userId;
            }
        }
    }
}

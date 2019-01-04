using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.Oauth2.AdminServer.ViewModel
{
    public class ClientViewModel
    {
        /// <summary>
        /// Key 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Client id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Client name
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Allow  refrest_token
        /// </summary>
        public bool AllowOfflineAccess { get; set; }

        /// <summary>
        /// Is enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}

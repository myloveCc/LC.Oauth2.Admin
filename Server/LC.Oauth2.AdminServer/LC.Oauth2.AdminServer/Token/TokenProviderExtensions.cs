using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.Oauth2.AdminServer
{
    /// <summary>
    /// TokenProvider扩展
    /// </summary>
    public static class TokenProviderExtensions
    {
        public static IApplicationBuilder UseTokenProvider(this IApplicationBuilder app, TokenProviderOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));
        }
    }
}

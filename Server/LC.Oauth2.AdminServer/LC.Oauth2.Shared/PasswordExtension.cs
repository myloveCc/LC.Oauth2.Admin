using System;
using NETCore.Encrypt;
using NETCore.Encrypt.Extensions;

namespace LC.Oauth2.Shared
{
    /// <summary>
    /// 密码扩展
    /// </summary>
    public static class PasswordExtension
    {
        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="password">原始密码</param>
        /// <returns>返回加密后密码</returns>
        public static string Encrypt(this string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var str = $"{password}.lc.oauth2.admin";

            return str.SHA1();
        }
    }
}

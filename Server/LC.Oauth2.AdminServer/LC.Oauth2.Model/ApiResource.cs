
using System;
using System.Collections.Generic;

namespace LC.Oauth2.Entities
{
    /// <summary>
    /// API资源
    /// </summary>
    public class ApiResource
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;
        /// <summary>
        /// API资源名称-鉴权使用
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// API资源 秘钥,可分配多个
        /// </summary>
        public List<ApiSecret> Secrets { get; set; }
        /// <summary>
        /// API资源 范围
        /// </summary>
        public List<ApiScope> Scopes { get; set; }
        /// <summary>
        /// API资源 用户权限
        /// </summary>
        public List<ApiResourceClaim> UserClaims { get; set; }
        public List<ApiResourceProperty> Properties { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? Updated { get; set; }

        public DateTime? LastAccessed { get; set; }
        public bool NonEditable { get; set; }
    }
}

// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

#pragma warning disable 1591

using System.Collections.Generic;

namespace LC.Oauth2.Entities
{
    /// <summary>
    /// API范围
    /// </summary>
    public class ApiScope
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// API Scope 名称
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
        /// 是否必须
        /// </summary>
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        /// <summary>
        /// 是否可发现
        /// </summary>
        public bool ShowInDiscoveryDocument { get; set; } = true;

        /// <summary>
        /// 关联用户身份
        /// </summary>
        public List<ApiScopeClaim> UserClaims { get; set; }

        /// <summary>
        /// API资源Id
        /// </summary>
        public int ApiResourceId { get; set; }

        /// <summary>
        /// API资源信息
        /// </summary>
        public ApiResource ApiResource { get; set; }
    }
}
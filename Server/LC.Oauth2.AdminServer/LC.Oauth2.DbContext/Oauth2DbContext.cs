using LC.Oauth2.DbOptions;
using LC.Oauth2.Entities;
using LC.Oauth2.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace LC.Oauth2.DbContext
{
    public class Oauth2DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public Oauth2DbContext(DbContextOptions options)
        : base(options)
        {

        }
       
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }
        public DbSet<ApiScope> ApiScopes { get; set; }
        public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }
        public DbSet<ApiSecret> ApiSecrets { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientClaim> ClientClaims { get; set; }
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        public DbSet<ClientGrantType> ClientGrantTypes { get; set; }
        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        public DbSet<ClientProperty> ClientProperties { get; set; }
        public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
        public DbSet<ClientScope> ClientScopes { get; set; }
        public DbSet<ClientSecret> ClientSecrets { get; set; }

        public DbSet<IdentityClaim> IdentityClaims { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }
        public DbSet<IdentityResourceProperty> IdentityResourceProperties { get; set; }

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        /// <summary>
        /// 系统用户表
        /// </summary>
        public DbSet<SysUser> SysUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureClientContext(new ConfigurationStoreOptions());
            modelBuilder.ConfigurePersistedGrantContext(new OperationalStoreOptions());
         
            base.OnModelCreating(modelBuilder);
        }
    }
}

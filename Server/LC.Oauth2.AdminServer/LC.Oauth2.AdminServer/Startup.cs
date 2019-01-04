using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.Oauth2.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LC.Oauth2.AdminServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //连接数据库
            services.AddDbContextPool<Oauth2DbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Oauth2DataBase"),
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("LC.Oauth2.AdminServer");
                });
            });

            //开启跨域请求
            services.AddCors();

            //版本控制
            services.AddApiVersioning();

            //JWT 认证
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //不使用https
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = TokenValidationParametersBuilder.Build(Configuration);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //CORS
            app.UseCors(builder =>
                builder.WithOrigins(Configuration["CorsOptions:Origins"])
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
            );

            //JWT Token provider
            app.UseTokenProvider(new TokenProviderOptions
            {
                Audience = Configuration["TokenOptions:Audience"],
                Issuer = Configuration["TokenOptions:Issuer"],
                SigningCredentials = SigningCredentialsBuilder.Build(Configuration)
            });

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

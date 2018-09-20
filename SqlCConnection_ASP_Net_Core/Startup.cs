using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlCConnection_ASP_Net_Core.Helper;
using SqlCConnection_ASP_Net_Core.Interfaces;
using SqlCConnection_ASP_Net_Core.Models;
using SqlCConnection_ASP_Net_Core.Repository;
using TobitLogger.Core;
using TobitLogger.Logstash;
using TobitLogger.Middleware;
using TobitWebApiExtensions.Extensions;

namespace SqlCConnection_ASP_Net_Core
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }
        //public ILoggerFactory LoggerFactory { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.Configure<DbSettings>(Configuration.GetSection("DbSettings"));
            services.Configure<ChaynsApiInfo>(Configuration.GetSection("ChaynsBackendApi"));

            services.AddChaynsToken();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILogContextProvider, RequestGuidContextProvider>();

            services.AddSingleton<IDbContext, DbContext>();
            services.AddScoped<ICompanyRepo, CompanyRepo>();
            services.AddScoped<IMessageHelper, MessageHelper>();
            services.AddScoped<IGroupHelper, GroupHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ILogContextProvider logContextProvider)
        {
            loggerFactory.AddLogstashLogger(Configuration.GetSection("LogstashLogger"), logContextProvider: logContextProvider);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseRequestLogging();

            
            app.UseMvc();
        }
    }
}

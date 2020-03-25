using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCore.Data.Repositories;
using NetCore.Data.Repositories.Parcels;
using NetCore.Services.Parcels;
using Microsoft.EntityFrameworkCore;

namespace NetCore.WebApi
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
            ConfigureService(services);
            ConfigureConfigurations(services);
            services.AddControllers();

            #region 注册Swagger服务
            services.AddSwaggerGen(c =>
                {
                    // 添加文档信息
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "NetCore WebAPI", Version = "v1" });
                });
            services.AddSwaggerGenNewtonsoftSupport();
            #endregion
        }

        #region Configure
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                #region 配置Swagger服务
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCore.WebAPI v1");
                });
                #endregion

                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        #endregion

        #region 服务注册
        public void ConfigureService(IServiceCollection services)
        {
            #region Dapper注册
            services.AddScoped<TMSConnectionFactory>(); //注入TMS连接
            services.AddScoped<DbConnectionFactory>(); //注入基础连接，用于下面的Repository泛型
            services.AddScoped<Repository<Data.Domain.Parcels.ParcelDo>>(); //注入Dapper仓储泛型
            services.AddScoped<ParcelRepository>(); //注入包裹仓储模型 
            #endregion

            #region EF注册
            services.AddDbContext<DataEF.ObjectContext>(o => o.UseSqlServer("server=10.10.4.201;User ID=sa;Password=colipu;database=NewTMSDB;Min Pool Size=100;Max Pool Size=200;"));
            services.AddScoped<DataEF.IDbContext, DataEF.ObjectContext>();
            services.AddScoped<NetCore.DataEF.IRepository<NetCore.Core.Domain.Parcels.Parcel>, NetCore.DataEF.EfRepository<NetCore.Core.Domain.Parcels.Parcel>>();
            #endregion

            services.AddScoped<ParcelService>(); ///注入业务层
        }
        #endregion

        #region 配置注册
        public void ConfigureConfigurations(IServiceCollection services)
        {
            //数据库连接配置读取
            services.Configure<DbConnectionConfig>(Configuration.GetSection("ConnectionStrings"));
        }
        #endregion
    }
}

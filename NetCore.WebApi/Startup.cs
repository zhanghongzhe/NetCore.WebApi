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

            #region ע��Swagger����
            services.AddSwaggerGen(c =>
                {
                    // ����ĵ���Ϣ
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
                #region ����Swagger����
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

        #region ����ע��
        public void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<TMSConnectionFactory>();
            services.AddScoped<ParcelRepository>();
            services.AddScoped<ParcelService>();

        }
        #endregion

        #region ����ע��
        public void ConfigureConfigurations(IServiceCollection services)
        {
            //���ݿ��������ö�ȡ
            services.Configure<DbConnectionConfig>(Configuration.GetSection("ConnectionStrings"));
        }
        #endregion
    }
}

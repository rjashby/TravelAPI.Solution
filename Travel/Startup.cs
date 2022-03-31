using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Travel.Models;
using Travel.Services;
using System.Reflection;
using System.IO;

namespace Travel
{
  #pragma warning disable CS1591
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
            ///versioning test
            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            ////end of versioning test
            
            services.AddDbContext<TravelContext>(opt =>
                opt.UseMySql(Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));
            services.AddControllers();


            // Add Pagination
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
              var accessor = o.GetRequiredService<IHttpContextAccessor>();
              var request = accessor.HttpContext.Request;
              var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
              return new UriService(uri);
            });
            // Finish Pagination

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
              c.SwaggerDoc("v1", new OpenApiInfo
              {
                Title = "Travel",
                Version = "v1",
                Description = "A simple example ASP.NET Core Web API",
                Contact = new OpenApiContact
                {
                  Name = "Bill Braski",
                  Email = "BillBill@gmail.com"
                }
              });
            });
            // End Swagger
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Travel v1"));
                // Swagger
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
  #pragma warning restore CS1591
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomTools.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CustomTools.Data.Access;
using CustomTools.Data.Access.DAL.Interfaces;
using CustomTools.Data.Access.DAL.Interfaces.Customer;
using CustomTools.Data.Access.DAL.Interfaces.Sustainability;
using CustomTools.Data.Access.DAL.Repositories;
using CustomTools.Data.Access.DAL.Repositories.Customer;
using CustomTools.Data.Access.DAL.Repositories.Sustainability;
using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.Server.IISIntegration;
using Swashbuckle.AspNetCore.Swagger;

namespace CustomTools.Api
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
            // Access to Customer DB Connection String
            //string dbConnectionString = this.Configuration.GetConnectionString("CustomerConnectionString");
            //services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

            // Access to Sustainability DB Connection String
            string sustainabilityDbConnectionString = this.Configuration.GetConnectionString("SustainabilityConnectionString");
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(sustainabilityDbConnectionString));

            // Access to Supplement DB Connection String
            //string supplementDbConnectionString = this.Configuration.GetConnectionString("SupplementConnectionString");
            //services.AddTransient<IDbConnection>((sp) => new SqlConnection(supplementDbConnectionString));

            // Register your repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISustainabilityRepository, SustainabilityRepository>();

            // For Windows Auth
            services.AddAuthentication(HttpSysDefaults.AuthenticationScheme);

            services.AddRazorPages();
            services.AddInfrastructure();
            services.AddControllers();

            // For Telerik
            services.AddKendo();

            services.AddAutoMapper(typeof(Mapper));
            services.AddMediatR(typeof(Startup));

            services.AddHttpContextAccessor();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen();

            services.AddMvcCore().AddApiExplorer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            { 
                app.UseHsts();
            }

            var swaggerConfig = new SwaggerConfig();
            Configuration.GetSection(nameof(SwaggerConfig)).Bind(swaggerConfig);

            app.UseSwagger(option => { option.RouteTemplate = swaggerConfig.JsonRoute; });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerConfig.UiEndpoint, swaggerConfig.Description);
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

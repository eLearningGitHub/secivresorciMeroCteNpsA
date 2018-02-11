using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductCatalogApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProductCatalogApi;
using Swashbuckle.AspNetCore;


namespace ProductCatalogApi
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
            services.Configure<CatalogSettings>(Configuration); // To make CatalogSettins injectable via IOptionsSnapshot
            /* 
            services.AddDbContext<CatalogContext>(options =>
                        options.UseSqlServer(Configuration["Catalog:ConnectionString"]));
            */

            /*
            var hostname = Environment.GetEnvironmentVariable("SQL_SERVER_HOST") ?? "sqlserver";
            var password = Environment.GetEnvironmentVariable("SA_PASSWORD") ?? "MyComplexPassword!234";
            var connectionString = $"Server={hostname};Database=CatalogDb;User ID=sa;Password={password}";
            services.AddDbContext<CatalogContext>(options =>
            {
                        options.UseSqlServer(connectionString);
            });
            */
            services.AddDbContext<CatalogContext>(options =>
                 options.UseSqlServer(Configuration["Catalog:ConnectionString"]));
            
            services.AddSwaggerGen(options => 
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info()
                {
                    Title = "ShoesOnContainer - ProductCatalog HTTP API",
                    Version = "v1",
                    Description = "The Catalog Microservice HTTP API. This is a Data-Driven/CRUD",
                    TermsOfService="Terim Of Service"
                });
                
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
                      
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger().UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json","My API v1");
                });
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}

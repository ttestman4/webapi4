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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace WebApi4
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    // Use camel case properties in the serializer and the spec (optional)
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    // Use string enums in the serializer and the spec (optional)
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            // Add NSwag OpenAPI/Swagger DI services and configure documents
            // For more advanced setup, see NSwag.Sample.NETCore20 project

            services.AddOpenApiDocument();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Add middlewares to service the OpenAPI/Swagger document and the web UI

            // URLs: 
            // - http://localhost:32367/swagger/a/swagger.json
            // - http://localhost:32367/swagger/b/swagger.json
            // - http://localhost:32367/swagger

            app.UseOpenApi(); // Serves the registered OpenAPI/Swagger documents by default on `/swagger/{documentName}/swagger.json`
            app.UseSwaggerUi3(); // registers a single Swagger UI (v3) with the two documents
        }
    }
}

﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.ReDoc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ConfigFromFile
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
            services.AddMvc();

            services.AddSwaggerGen();

            services.Configure<SwaggerGenOptions>(c =>
            {
                c.SwaggerDoc("v1", new Info { Version = "V1", Title = "My API" });
            });

            services.Configure<SwaggerOptions>(Configuration.GetSection("Swagger"));

            services.Configure<SwaggerUIOptions>(c =>
            {
                Configuration.Bind("SwaggerUI", c);

                c.ConfigObject.SupportedSubmitMethods = new SubmitMethod[]{ };
                c.ConfigObject.AdditionalItems.Add("swaggerUIFoo", "bar");
            });

            services.Configure<ReDocOptions>(c =>
            {
                Configuration.Bind("ReDoc", c);

                c.ConfigObject.AdditionalItems.Add("redocFoo", "bar");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseReDoc();
        }
    }
}
using System.IO;
using Bonbonniere.Infrastructure;
using Bonbonniere.Infrastructure.EFData;
using Bonbonniere.Infrastructure.Environment;
using Bonbonniere.Services;
using Bonbonniere.WebApis.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Bonbonniere.WebApis
{
    public class Startup
    {
        private readonly string _Project_Name = string.Empty;
        private readonly string _ApiVersion = string.Empty;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _Project_Name = Configuration["Settings:ProjectName"];
            _ApiVersion = Configuration["Settings:ApiVersion"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins(Configuration["Settings:Origins"]?.Split(";"))
                    .AllowCredentials().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddMemoryCache();

            services.AddMvc();

            services.Configure<EnvSettings>(Configuration.GetSection("Settings"));
            services.RegisterInfrastructureModule(Configuration.GetSection("Settings").Get<EnvSettings>());
            services.RegisterServiceModule();

            // Add cookie middleware to the service collection and configure it
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = new PathString("/Account/Login"));

            services.AddSwaggerGen(sg =>
            {
                sg.SwaggerDoc(_ApiVersion, new Info
                {
                    Version = _ApiVersion,
                    Title = $"{_Project_Name} Apis",
                    Description = $"RESTful Apis for {_Project_Name}",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Denis", Url = "" }
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, $"{_Project_Name}.WebApis.xml");
                sg.IncludeXmlComments(xmlPath);

                sg.OperationFilter<SwaggerHttpHeaderOperation>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, BonbonniereContext bonbonniereContext, ThirdPartyContext thirdPartyContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add the authentication middleware to the pipeline
            app.UseAuthentication();

            app.UseCors("AllowSpecificOrigin");

            //app.UseBasicAuthenticationMiddleware();

            app.UseSwagger();

            app.UseSwaggerUI(sg =>
            {
                sg.SwaggerEndpoint($"/swagger/{_ApiVersion}/swagger.json", $"{_Project_Name} {_ApiVersion}");
                sg.ShowRequestHeaders();
            });

            //app.UseStaticFiles();
            //app.UseStatusCodePages();

            app.UseMvc();

            bonbonniereContext.Initialize();
            thirdPartyContext.Initialize();
        }
    }
}

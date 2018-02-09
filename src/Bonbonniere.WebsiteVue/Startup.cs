using Bonbonniere.Infrastructure;
using Bonbonniere.Infrastructure.EFData;
using Bonbonniere.Infrastructure.Environment;
using Bonbonniere.Services;
using Bonbonniere.WebsiteVue.ApiControllers;
using Bonbonniere.WebsiteVue.MvcMiddlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;

namespace Bonbonniere.WebsiteVue
{
    public class Startup
    {
        private string _Project_Name = "Bonbonniere";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //_Project_Name = Configuration["Settings:ProjectName"];
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

            //// Add cookie middleware to the service collection and configure it
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options => options.LoginPath = new PathString("/Account/Login"));

            services.AddSwaggerGen(sg =>
            {
                typeof(ApiVersions)
                .GetEnumNames()
                .OrderByDescending(v => v)
                .ToList().ForEach(version =>
                {
                    sg.SwaggerDoc(version, new Info
                    {
                        Version = version,
                        Title = $"{_Project_Name} Apis",
                        Description = $"RESTful Apis for {_Project_Name}",
                        TermsOfService = "None",
                        Contact = new Contact { Name = "Denis", Url = "" }
                    });
                });
                
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, $"{_Project_Name}.WebsiteVue.xml");
                sg.IncludeXmlComments(xmlPath);

                sg.OperationFilter<HttpHeaderOperation>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, BonbonniereContext bonbonniereContext, ThirdPartyContext thirdPartyContext)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Add the authentication middleware to the pipeline
            app.UseAuthentication();

            app.UseCors("AllowSpecificOrigin");

            //app.UseBasicAuthenticationMiddleware();

            app.UseSwagger();

            app.UseSwaggerUI(sg =>
            {
                typeof(ApiVersions)
                .GetEnumNames()
                .OrderByDescending(v => v)
                .ToList()
                .ForEach(version => {
                    sg.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{_Project_Name} {version}");
                    sg.ShowRequestHeaders();
                });
            });

            app.UseStaticFiles();
            //app.UseStatusCodePages();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            bonbonniereContext.Initialize();
            thirdPartyContext.Initialize();
        }
    }
}

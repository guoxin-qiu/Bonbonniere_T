using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bonbonniere.Infrastructure.Environment;
using Bonbonniere.Infrastructure;
using Bonbonniere.Services;
using Microsoft.Extensions.Logging;
using Bonbonniere.Infrastructure.EFData;
using NLog.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Bonbonniere.Website
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
                .AddJsonOptions(t => t.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat) // TODO: not work
                //.AddJsonOptions(options =>
                //{
                //    if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                //    {
                //        resolver.NamingStrategy = null;
                //    }
                //}) // Camel to Pascal
                //.AddMvcOptions(options =>
                //{
                //    options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                //})
                ;

            services.Configure<EnvSettings>(Configuration.GetSection("Settings"));
            services.RegisterInfrastructureModule(Configuration.GetSection("Settings").Get<EnvSettings>());
            services.RegisterServiceModule();

            // Add cookie middleware to the service collection and configure it
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = new PathString("/Account/SignIn"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory, BonbonniereContext bonbonniereContext, ThirdPartyContext thirdPartyContext)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Add the authentication middleware to the pipeline
            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            bonbonniereContext.Initialize();
            thirdPartyContext.Initialize();
        }
    }
}

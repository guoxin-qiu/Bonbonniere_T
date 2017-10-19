using Bonbonniere.Infrastructure.EFData;
using Bonbonniere.Infrastructure.Environment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bonbonniere.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static void RegisterInfrastructureModule(this IServiceCollection services, EnvSettings settings)
        {
            services.AddSingleton<IClock, Clock>();

            services.AddDbContext<BonbonniereContext>(o =>
                o.UseSqlServer(settings.BonbonniereConnection, b => b.MigrationsAssembly("Bonbonniere.Data")));
                //o.UseInMemoryDatabase("BonbonniereInMemory"));

            services.AddDbContext<ThirdPartyContext>(o =>
                o.UseSqlServer(settings.ThirdPartyConnection, b => b.MigrationsAssembly("Bonbonniere.Data")));
                //o.UseInMemoryDatabase("ThirdPartyInMemory"));
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Bonbonniere.Services
{
    public static class ServiceRegistation
    {
        public static void RegisterServiceModule(this IServiceCollection services)
        {
            var assembly = typeof(ServiceRegistation).GetTypeInfo().Assembly;

            var query = (from type in assembly.GetExportedTypes().Where(t => t.Name.EndsWith("Service"))
                         let matchingInterface = type.GetInterfaces().FirstOrDefault(t => t.Name.EndsWith("Service"))
                         where matchingInterface != null
                         select new { matchingInterface, type }).ToList();

            foreach (var pair in query)
            {
                services.AddScoped(pair.matchingInterface, pair.type);
            }
        }
    }
}

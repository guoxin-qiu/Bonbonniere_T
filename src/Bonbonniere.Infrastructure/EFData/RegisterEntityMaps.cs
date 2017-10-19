using Bonbonniere.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Bonbonniere.Infrastructure.EFData
{
    public static class DbContextRegister
    {
        public static void RegisterEntities(this ModelBuilder modelBuilder, string assemblyName, string namespaceOfEntity)
        {
            var entityTypes = Assembly.Load(assemblyName).GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace)
                    && type.Namespace.Contains(namespaceOfEntity)
                    && typeof(IAggregateRoot).IsAssignableFrom(type) && type.IsClass).ToList();

            foreach (var item in entityTypes)
            {
                if (modelBuilder.Model.FindEntityType(item) == null)
                {
                    modelBuilder.Model.AddEntityType(item);
                }
            }
        }

        public static void RegisterMaps(this ModelBuilder modelBuilder,string namespaceOfMap)
        {
            var entityMaps = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrWhiteSpace(type.Namespace)
                    && type.Namespace.Contains(namespaceOfMap)
                    && typeof(IEntityTypeMap).IsAssignableFrom(type) && type.IsClass).ToList();

            foreach (var item in entityMaps)
            {
                Activator.CreateInstance(item, modelBuilder);
            }
        }
    }
}

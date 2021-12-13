using CharityPlatform.DAL;
using CharityPlatform.Integration.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CharityPlatform.Integration
{
    public static class CharityPlatformIntegrationRegistry
    {
        public static void RegisterCharityPlatformIntegration(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped(s => new StoredEventSerializer(assemblies));
            services.AddMediatR(typeof(CharityPlatformIntegrationRegistry).Assembly, typeof(CharityPlatformDalRegistry).Assembly);
        }
    }
}

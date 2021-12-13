using CharityPlatform.DAL.Interfaces;
using CharityPlatform.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CharityPlatform.DAL
{
    public static class CharityPlatformDalRegistry
    {
        public static void RegisterCharityPlatformDal(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CharityPlatformContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<ReadModelReader>();
        }
    }
}

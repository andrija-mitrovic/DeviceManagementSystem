using Application.Common.Interfaces;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        private const string CONNECTION_STRING_NAME = "SqlServer";

        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddServices();
        }
        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(CONNECTION_STRING_NAME),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<ApplicationDbContextInitialiser>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IDeviceTypeRepository, DeviceTypeRepository>();
            services.AddScoped<IDeviceTypePropertyRepository, DeviceTypePropertyRepository>();
            services.AddScoped<IDevicePropertyValueRepository, DevicePropertyValueRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IDateTime, DateTimeService>();
        }
    }
}

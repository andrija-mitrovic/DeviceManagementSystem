using API.Filters;
using API.Services;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static void AddAPIServices(this IServiceCollection services)
        {
            services.ConfigureController();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddServices();
            services.AddHttpContextAccessor();
            services.ConfigureApiBehaviorOptions();
        }

        private static void ConfigureController(this IServiceCollection services)
        {
            services.AddControllers(opt => opt.Filters.Add<ApiExceptionFilterAttribute>())
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddScoped<ValidationFilterAttribute>();
        }

        private static void ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}

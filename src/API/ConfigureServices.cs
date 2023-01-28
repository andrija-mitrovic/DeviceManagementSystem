using API.Filters;
using API.Services;
using Application.Common.Interfaces;

namespace API
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
        }
    }
}

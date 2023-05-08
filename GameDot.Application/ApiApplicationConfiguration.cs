using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace GameDot.Application
{
    public static class ApiApplicationConfiguration
    {
        public static IServiceCollection AddApiApplication(this IServiceCollection services)
        {
            services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}


using Microsoft.Extensions.DependencyInjection;
namespace Project.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            //object value = services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}

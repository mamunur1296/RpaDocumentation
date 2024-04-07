using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Mapper;
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

using QrMenu.Infrastructure.Adapters.ImageService;
using Microsoft.Extensions.DependencyInjection;

namespace QrMenu.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
       // services.AddScoped<ImageServiceBase, CloudinaryImageServiceAdapter>();
        return services;
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.OrderMicroservice.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your business logic layer services here, e.g., services, validators, etc.
        return services;
    }
}


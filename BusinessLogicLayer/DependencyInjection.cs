using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.Validators;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your business logic layer services here, e.g., services, validators, etc.
        services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();
        return services;
    }
}


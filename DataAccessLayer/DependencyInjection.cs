using eCommerce.OrdersMicroservice.DataAccessLayer.Repositories;
using eCommerce.OrdersMicroservice.DataAccessLayer.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace eCommerce.OrdersMicroservice.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your data access layer services here, e.g., DbContext, repositories, etc.

        string connectionStringTemplate = configuration.GetConnectionString("MongoDB")!;
        string connectionString = connectionStringTemplate
            .Replace("$MONGO_HOST", Environment.GetEnvironmentVariable("MONGODB_HOST")!)
            .Replace("$MONGO_PORT", Environment.GetEnvironmentVariable("MONGODB_PORT")!);

        services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
        services.AddScoped<IMongoDatabase>(IConfigurationProvider =>
        {
            IMongoClient mongoClient = IConfigurationProvider.GetRequiredService<IMongoClient>();
            return mongoClient.GetDatabase("eCommerceOrdersDB");
        });

            services.AddScoped<IOrdersRepository, OrdersRepository>();


        return services;
    }
}

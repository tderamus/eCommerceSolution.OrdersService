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
            .Replace("$MONGODB_HOST", Environment.GetEnvironmentVariable("MONGODB_HOST")!)
            .Replace("$MONGODB_PORT", Environment.GetEnvironmentVariable("MONGODB_PORT")!)
            .Replace("$MONGODB_DATABASE", Environment.GetEnvironmentVariable("MONGODB_DATABASE")!);

        services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
        services.AddScoped<IMongoDatabase>(provider =>
        {
            IMongoClient mongoClient = provider.GetRequiredService<IMongoClient>();
            return mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_DATABASE")!);
        });

            services.AddScoped<IOrdersRepository, OrdersRepository>();


        return services;
    }
}

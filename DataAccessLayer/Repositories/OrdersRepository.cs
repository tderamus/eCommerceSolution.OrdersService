using eCommerce.OrderMicroservice.DataAccessLayer.Entities;
using eCommerce.OrderMicroservice.DataAccessLayer.RepositoryContracts;
using MongoDB.Driver;

namespace eCommerce.OrderMicroservice.DataAccessLayer.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly IMongoCollection<Order> _orders;
    private readonly string _collectionName = "Orders";
    public OrdersRepository(IMongoDatabase mongoDatabase)
    {
         _orders = mongoDatabase.GetCollection<Order>(_collectionName);
    }
    public async Task<Order?> CreateOrder(Order order)
    {
        order.OrderId = Guid.NewGuid();

        await _orders.InsertOneAsync(order);
        return order;
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {
       FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderId, orderID);

        Order? orderToDelete = (await _orders.FindAsync(filter)).FirstOrDefault();

        if (orderToDelete == null)
        {
            return false; // Order not found
        }

        DeleteResult deleteResult = await _orders.DeleteOneAsync(filter);

        return deleteResult.DeletedCount > 0;

    }

    public async Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        return (await _orders.FindAsync(filter)).FirstOrDefault();
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return (await _orders.FindAsync(Builders<Order>.Filter.Empty)).ToList();
    }

    public async Task<IEnumerable<Order?>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        return (await _orders.FindAsync(filter)).ToList();
    }

    public async Task<Order?> UpdateOrder(Order order)
    {
        FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderId, order.OrderId);

        Order? orderToUpdate= (await _orders.FindAsync(filter)).FirstOrDefault();

        if (orderToUpdate == null)
        {
            return null; // Order not found
        }

        ReplaceOneResult replaceOneResult = await _orders.ReplaceOneAsync(filter, order);

        return order;
    }
}


using eCommerce.OrderMicroservice.DataAccessLayer.Entities;
using MongoDB.Driver;

namespace eCommerce.OrderMicroservice.DataAccessLayer.RepositoryContracts;

public interface IOrdersRepository
{
    // Get all orders
    Task<IEnumerable<Order>> GetOrders();

    // Get orders by condition
    Task<IEnumerable<Order?>> GetOrdersByCondition(FilterDefinition<Order> filter);

    // Get single order by condition
    Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter);

    // Create a new order
    Task<Order?> CreateOrder(Order order);

    // Update an existing order
    Task<Order?> UpdateOrder(Order order);

    // Delete an order
    Task<bool> DeleteOrder(Guid orderID);
}
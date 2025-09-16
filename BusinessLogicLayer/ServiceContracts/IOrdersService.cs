
using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrdersMicroservice.DataAccessLayer.Entities;
using MongoDB.Driver;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.ServiceContracts
{
    public interface IOrdersService
    {
        Task<List<OrderResponse?>> GetOrders();
        Task<List<OrderResponse?>> GetOrdersByCondition(FilterDefinition<Order> filter);
        Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter);
        Task<OrderResponse> CreateOrder(OrderAddRequest orderAddRequest);
        Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest);
        Task<bool> DeleteOrder(Guid orderID);
    }
}

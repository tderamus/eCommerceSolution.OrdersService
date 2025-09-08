
using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrdersMicroservice.DataAccessLayer.Entities;
using MongoDB.Driver;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.ServiceContracts
{
    public interface IOrdersService
    {
        Task<List<OrderResponse>> GetAllOrdersAsync();
        Task<OrderResponse> GetOrdersByConditionAsync(FilterDefinition<Order> filter);
        Task<OrderResponse> GetOrderByConditionAsync(FilterDefinition<Order> filter);
        Task<OrderResponse> CreateOrderAsync(OrderAddRequest orderAddRequest);
        Task<OrderResponse> UpdateOrderAsync(OrderUpdateRequest orderUpdateRequest);
        Task<bool> DeleteOrderAsync(Guid orderID);
    }
}

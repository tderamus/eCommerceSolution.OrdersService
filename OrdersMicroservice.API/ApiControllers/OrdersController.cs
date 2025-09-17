using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.ServiceContracts;
using eCommerce.OrdersMicroservice.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace OrdersMicroservice.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IEnumerable<OrderResponse?>> Get()
        {
            List<OrderResponse?> orders =  await _ordersService.GetOrders();
            return orders;
        }

        // GET: api/Orders/search/orderId/{orderID}
        [HttpGet("search/orderId/{orderID}")]
        public async Task<OrderResponse?> GetOrderByOrderId(Guid orderID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderId, orderID);

            OrderResponse? order = await _ordersService.GetOrderByCondition(filter);
            return order;
        }

        // GET: api/orders/search/productID/{productID}
        [HttpGet("search/productId/{productID}")]
        public async Task<IEnumerable<OrderResponse?>> GetOrderByProductId(Guid productID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.ElemMatch(o => o.OrderItems,
                Builders<OrderItem>.Filter.Eq(tempProduct => tempProduct.ProductId, productID));

            List<OrderResponse?> orders = await _ordersService.GetOrdersByCondition(filter);
            return orders;
        }

        // GET: api/Orders/search/orderDate/{orderDate}
        [HttpGet("search/orderDate/{orderDate}")]
        public async Task<IEnumerable<OrderResponse?>> GetOrdersByOrderDate(DateTime orderDate)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(od => od.OrderDate.ToString("yyyy-MM-dd"), orderDate.ToString("yyyy-MM-dd"));

            List<OrderResponse?> orders = await _ordersService.GetOrdersByCondition(filter);
            return orders;
        }
    }
}

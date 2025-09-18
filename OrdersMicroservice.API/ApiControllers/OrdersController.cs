using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.ServiceContracts;
using eCommerce.OrdersMicroservice.DataAccessLayer.Entities;
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
            List<OrderResponse?> orders = await _ordersService.GetOrders();
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

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> Post(OrderAddRequest orderAddRequest)
        {
            if (orderAddRequest == null)
            {
                return BadRequest("Invalid order data");
            }

            OrderResponse? orderResponse = await _ordersService.CreateOrder(orderAddRequest);

            if (orderResponse == null)
            {
                return Problem("Failed to create order");
            }

            return Created($"api/Orders/search/orderId/{orderResponse.OrderID}", orderResponse);
        }

        // PUT: api/Orders/{orderID}
        [HttpPut("{orderId}")]
        public async Task<IActionResult> Put(Guid orderID, OrderUpdateRequest orderUpdateRequest)
        {
            if (orderUpdateRequest == null)
            {
                return BadRequest("Invalid order data");
            }

            if (orderID != orderUpdateRequest.OrderID)
            {
                return BadRequest("Order ID from URL does not match Order ID from request body");
            }

            OrderResponse? orderResponse = await _ordersService.UpdateOrder(orderUpdateRequest);
            if (orderResponse == null)
            {
                return NotFound($"Order with ID {orderUpdateRequest.OrderID} not found");
            }
            return Ok(orderResponse);
        }

        // DELETE: api/Orders/{orderID}
        [HttpDelete("{orderID}")]
        public async Task<IActionResult> Delete(Guid orderID)
        {
            if (orderID == Guid.Empty)
            {
                return BadRequest("Invalid order ID");
            }

            bool isDeleted = await _ordersService.DeleteOrder(orderID);
            if (!isDeleted)
            {
                return NotFound($"Order with ID {orderID} not found");
            }
            return Ok(isDeleted);
        }

        // GET: api/Orders/search/userId/{userID}
        [HttpGet("search/userId/{userID}")]
        public async Task<IEnumerable<OrderResponse?>> GetOrdersByUserId(Guid userID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.UserId, userID);
            List<OrderResponse?> orders = await _ordersService.GetOrdersByCondition(filter);
            return orders;
        }
    }
}

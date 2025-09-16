using AutoMapper;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.ServiceContracts;
using eCommerce.OrdersMicroservice.DataAccessLayer.Entities;
using eCommerce.OrdersMicroservice.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using MongoDB.Driver;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<OrderAddRequest> _orderAddRequestValidator;
    private readonly IValidator<OrderItemAddRequest> _orderItemAddRequestValidator;
    private readonly IValidator<OrderUpdateRequest> _orderUpdateRequestValidator;
    private readonly IValidator<OrderItemUpdateRequest> _orderItemUpdateRequestValidator;

    public OrdersService(IOrdersRepository ordersRepository, IMapper mapper,
        IValidator<OrderAddRequest> orderAddRequestValidator,
        IValidator<OrderItemAddRequest> orderItemAddRequestValidator,
        IValidator<OrderUpdateRequest> orderUpdateRequestValidagor,
        IValidator<OrderItemUpdateRequest> orderItemUpdateRequestValidator)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
        _orderAddRequestValidator = orderAddRequestValidator;
        _orderItemAddRequestValidator = orderItemAddRequestValidator;
        _orderUpdateRequestValidator = orderUpdateRequestValidagor;
        _orderItemUpdateRequestValidator = orderItemUpdateRequestValidator;
    }

    public async Task<OrderResponse> CreateOrder(OrderAddRequest orderAddRequest)
    {
        //Check for null parameter
        if (orderAddRequest == null)
        {
            throw new ArgumentNullException(nameof(orderAddRequest));
        }
        // Validate the orderAddRequest using FluentValidation
        var validationResult = await _orderAddRequestValidator.ValidateAsync(orderAddRequest);
        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException($"Invalid order add request: {errors}");
        }
        //Validate each OrderItemAddRequest in the OrderAddRequest using FluentValidation
        foreach (OrderItemAddRequest orderItemAddRequest in orderAddRequest.OrderItems)
        {
            var orderItemAddRequestValidationResult = await _orderItemAddRequestValidator.ValidateAsync(orderItemAddRequest);
            if (!orderItemAddRequestValidationResult.IsValid)
            {
                string errors = string.Join(", ", orderItemAddRequestValidationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Invalid order item add request: {errors}");
            }
        }

        //TODO: Check if the CustomerId exists in the Customers microservice
        //TODO: Check if the UserId exists in the Users microservice

        // Map the OrderAddRequest DTO to the Order entity
        Order orderInput = _mapper.Map<Order>(orderAddRequest);
        
        foreach(OrderItem orderItem in orderInput.OrderItems)
        {
           orderItem.TotalPrice = orderItem.UnitPrice * orderItem.Quantity;
        }
        orderInput.TotalAmount = orderInput.OrderItems.Sum(oi => oi.TotalPrice);

        // Call the repository to add the order
        Order? addedOrder = await _ordersRepository.CreateOrder(orderInput);
        // If the order was not added, return null
        if (addedOrder == null)
        {
            return null;
        }
        // Map the added Order entity to the OrderResponse DTO
        OrderResponse addedOrderResponse = _mapper.Map<OrderResponse>(addedOrder);
        return addedOrderResponse;
    }

    public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
    {
        //Check for null parameter
        if (orderUpdateRequest == null)
        {
            throw new ArgumentNullException(nameof(orderUpdateRequest));
        }
        // Validate the orderUpdateRequest using FluentValidation
        var validationResult = await _orderUpdateRequestValidator.ValidateAsync(orderUpdateRequest);
        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException($"Invalid order add request: {errors}");
        }
        //Validate each OrderUpdateRequest in the OrderUpdateRequest using FluentValidation
        foreach (OrderItemUpdateRequest orderItemUpdateRequest in orderUpdateRequest.OrderItems)
        {
            var orderItemUpdateRequestValidationResult = await _orderItemUpdateRequestValidator.ValidateAsync(orderItemUpdateRequest);
            if (!orderItemUpdateRequestValidationResult.IsValid)
            {
                string errors = string.Join(", ", orderItemUpdateRequestValidationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Invalid order item add request: {errors}");
            }
        }

        //TODO: Check if the CustomerId exists in the Customers microservice
        //TODO: Check if the UserId exists in the Users microservice

        // Map the OrderAddRequest DTO to the Order entity
        Order orderInput = _mapper.Map<Order>(orderUpdateRequest);

        foreach (OrderItem orderItem in orderInput.OrderItems)
        {
            orderItem.TotalPrice = orderItem.UnitPrice * orderItem.Quantity;
        }
        orderInput.TotalAmount = orderInput.OrderItems.Sum(oi => oi.TotalPrice);

        // Call the repository to add the order
        Order? updatedOrder = await _ordersRepository.UpdateOrder(orderInput);
        // If the order was not added, return null
        if (updatedOrder == null)
        {
            return default;
        }
        // Map the added Order entity to the OrderResponse DTO
        OrderResponse updatedOrderResponse = _mapper.Map<OrderResponse>(updatedOrder);
        return updatedOrderResponse;
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {
        //Create filter to find the order by orderID
        FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderId, orderID);
        Order? existingOrder = await _ordersRepository.GetOrderByCondition(filter);

        if (existingOrder == null)
        {
            return false;
        }
        bool isDeleted = await _ordersRepository.DeleteOrder(orderID);
        return isDeleted;
    }

    public async Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        //Check for null parameter
        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));
        }
        Order? order = await _ordersRepository.GetOrderByCondition(filter);
        if (order == null)
        {
            return default;
        }
        OrderResponse orderResponse = _mapper.Map<OrderResponse>(order);
        return orderResponse;
    }

    public async Task<List<OrderResponse?>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        //Check for null parameter
        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));
        }
        IEnumerable<Order?> orders = await _ordersRepository.GetOrdersByCondition(filter);
        IEnumerable<OrderResponse?> orderResponses = _mapper.Map<IEnumerable<OrderResponse>>(orders);
        return orderResponses.ToList();
    }

    public async Task<List<OrderResponse?>> GetOrders()
    {
        IEnumerable<Order> orders = await _ordersRepository.GetOrders();
        IEnumerable<OrderResponse?> orderResponses = _mapper.Map<IEnumerable<OrderResponse>>(orders);
        return orderResponses.ToList();
    }

}
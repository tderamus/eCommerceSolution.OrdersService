
namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;

public record OrderItemAddRequest(Guid ProductID, int Quantity, decimal UnitPrice)
{
    public OrderItemAddRequest(): this(default, default, default)
    { 
        
    }
}
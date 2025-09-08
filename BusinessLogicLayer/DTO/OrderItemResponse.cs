
namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;

public record OrderItemResponse(Guid ProductID, int Quantity, decimal UnitPrice, decimal TotalPrice)
{
    public OrderItemResponse() : this(default, default, default, default)
    {

    }
}
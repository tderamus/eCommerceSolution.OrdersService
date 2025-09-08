
namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;

public record OrderItemUpdateRequest(Guid ProductID, int Quantity, decimal UnitPrice)
{
    public OrderItemUpdateRequest() : this(default, default, default)
    {

    }
}
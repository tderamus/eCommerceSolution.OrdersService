
using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.Validators;

public class OrderItemAddRequestValidator : AbstractValidator<OrderItemAddRequest>
{
    public OrderItemAddRequestValidator()
    {
        RuleFor(x => x.ProductID)
            .NotEmpty().WithErrorCode("Product ID is required.")
            .Must(id => id != Guid.Empty).WithErrorCode("Product ID cannot be an empty GUID.");
        RuleFor(x => x.Quantity)
            .NotEmpty().WithErrorCode("Quantity is required.")
            .GreaterThan(0).WithErrorCode("Quantity must be greater than zero.");
        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithErrorCode("Price must be greater than zero.");
    }
}

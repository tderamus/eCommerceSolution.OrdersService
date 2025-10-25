
using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.Validators;

public class OrderAddRequestValidator : AbstractValidator<OrderAddRequest>
{
    public OrderAddRequestValidator()
    {
        RuleFor(x => x.UserID)
            .NotEmpty().WithErrorCode("User ID is required.")
            .Must(id => id != Guid.Empty).WithErrorCode("User ID cannot be an empty GUID.");

        RuleFor(x => x.OrderDate)
            .NotEmpty().WithErrorCode("Order date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithErrorCode("Order date cannot be in the future.");

        RuleFor(x => x.OrderItems)
            .NotEmpty().WithErrorCode("Order must contain at least one item.")
            .Must(items => items != null && items.Count > 0).WithErrorCode("Order must contain at least one item.");
    }
}

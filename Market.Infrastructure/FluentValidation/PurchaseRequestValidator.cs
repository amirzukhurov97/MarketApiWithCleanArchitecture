using FluentValidation;
using Market.Application.DTOs.Purchase;

namespace MarketApi.FluentValidation
{
    public class PurchaseRequestValidator : AbstractValidator<PurchaseRequest>
    {
        public PurchaseRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Total price must be greater than zero.");
           
        }
    }
}

using Cinephila.Domain.Models.ProductionModels;
using FluentValidation;

namespace Cinephila.API.FluentValidators
{
    public class ProductionModelValidator : AbstractValidator<ProductionModel>
    {
        public ProductionModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Summary).NotEmpty();
        }
    }
}

using Cinephila.Domain.Models.ProductionModels;
using FluentValidation;

namespace Cinephila.API.FluentValidators
{
    public class ProductionCreateModelValidator : AbstractValidator<ProductionCreateModel>
    {
        public ProductionCreateModelValidator()
        {
            RuleFor(x => x).Must(x => (x.Movie != null && x.TVShow == null) || (x.Movie == null && x.TVShow != null))
                .WithMessage("One of the properties should be null!");
        }
    }
}

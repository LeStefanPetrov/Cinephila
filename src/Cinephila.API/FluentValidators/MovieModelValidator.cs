using Cinephila.Domain.Models.ProductionModels;
using FluentValidation;

namespace Cinephila.API.FluentValidators
{
    public class MovieModelValidator : AbstractValidator<MovieModel>
    {
        public MovieModelValidator()
        {
            RuleFor(x => x.LengthInMinutes).NotEmpty();
        }
    }
}

using Cinephila.Domain.Models.ProductionModels;
using FluentValidation;

namespace Cinephila.API.FluentValidators
{
    public class TVShowModelValidator : AbstractValidator<TVShowModel>
    {
        public TVShowModelValidator()
        {
        }
    }
}

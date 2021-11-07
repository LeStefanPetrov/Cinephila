using Cinephila.Domain.Models.ParticipantModels;
using FluentValidation;

namespace Cinephila.API.FluentValidators
{
    public class ParticipantModelValidator : AbstractValidator<ParticipantModel>
    {
        public ParticipantModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.BirthDate).NotEmpty();
        }
    }
}

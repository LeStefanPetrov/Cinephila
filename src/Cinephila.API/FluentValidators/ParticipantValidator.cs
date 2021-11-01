using Cinephila.Domain.DTOs.ParticipantDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.API.FluentValidators
{
    public class ParticipantValidator : AbstractValidator<ParticipantDto>
    {
        public ParticipantValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.BirthDate).NotEmpty();
        }
    }
}

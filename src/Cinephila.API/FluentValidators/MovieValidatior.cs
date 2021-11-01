using Cinephila.Domain.DTOs.ProductionDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinephila.API.FluentValidators
{
    public class MovieValidatior : AbstractValidator<MovieDto>
    {
        public MovieValidatior()
        {
            //RuleFor(x => x.Name).NotEmpty();
            //RuleFor(x => x.LengthInMinutes).NotEmpty();
        }
    }
}

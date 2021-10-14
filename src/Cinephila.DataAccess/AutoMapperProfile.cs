using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ParticipantsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephila.DataAccess
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ParticipantDto, Participant>().ReverseMap();
        }
    }
}

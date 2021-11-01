using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;

namespace Cinephila.DataAccess
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ParticipantDto, Participant>().ReverseMap();

            CreateMap<MovieDto, Production>()
                .ForMember(x => x.Name, opts => opts.MapFrom(x => x.Name))
                .ForMember(x => x.YearOfCreation, opts => opts.MapFrom(x => x.YearOfCreation))
                .ForMember(x => x.Summary, opts => opts.MapFrom(x => x.Summary))
                .ForMember(x => x.Countries, opts => opts.MapFrom(x => x.Countries))
                .ForMember(x => x.ParticipantsProductions, opts => opts.MapFrom(x => x.Participants));

            CreateMap<int, CountryProduction>()
                .ForMember(x => x.CountryID, opts => opts.MapFrom(x => x));

            CreateMap<ParticipantRoleDto, ParticipantProduction>()
                .ForMember(x => x.ParticipantID, opts => opts.MapFrom(x => x.ParticipantID))
                .ForMember(x => x.RoleID, opts => opts.MapFrom(x => x.RoleID));

            CreateMap<MovieDto, Movie>()
                .ForMember(x => x.LengthInMinutes, opts => opts.MapFrom(x => x.LengthInMinutes))
                .ForMember(x => x.Production, opts => opts.MapFrom(x => x));

            CreateMap<TVShowDto, TVShow>()
                .ForMember(x => x.EndOfProduction, opts => opts.MapFrom(x => x.EndOfProduction))
                .ForMember(x => x.Production, opts => opts.MapFrom(x => new Production { Name = x.Name, YearOfCreation = x.YearOfCreation, Summary = x.Summary }));
        }
    }
}

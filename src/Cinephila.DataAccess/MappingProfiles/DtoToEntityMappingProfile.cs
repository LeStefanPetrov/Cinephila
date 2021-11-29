using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;
using System.Collections.Generic;
using System.Linq;

namespace Cinephila.DataAccess.MappingProfiles
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {
            CreateMap<Participant, ParticipantEntity>().ReverseMap();

            CreateMap<int, CountryProductionEntity>()
                .ForMember(x => x.CountryID, opts => opts.MapFrom(x => x));

            CreateMap<ParticipantRole, ParticipantProductionEntity>()
                .ForMember(x => x.ParticipantID, opts => opts.MapFrom(x => x.ParticipantID))
                .ForMember(x => x.RoleID, opts => opts.MapFrom(x => x.RoleID));

            CreateMap<Movie, MovieEntity>()
                .ForMember(x => x.LengthInMinutes, opts => opts.MapFrom(x => x.LengthInMinutes))
                .ForMember(x => x.Production, opts => opts.MapFrom(x => x));

            CreateMap<TVShow, TVShowEntity>()
                .ForMember(x => x.EndOfProduction, opts => opts.MapFrom(x => x.EndOfProduction))
                .ForMember(x => x.Production, opts => opts.MapFrom(x => x));

            CreateMap<Production, ProductionEntity>().ConvertUsing<ProductionToProductionEntityResolver>();
        }
    }

    public class ProductionToProductionEntityResolver : ITypeConverter<Production, ProductionEntity>
    {
        public ProductionEntity Convert(Production source, ProductionEntity destination, ResolutionContext context)
        {
            destination.Name = source.Name;
            destination.Summary = source.Summary;
            destination.YearOfCreation = source.YearOfCreation;

            foreach (var participant in destination.ParticipantsProductions)
            {
                if (!source.Participants.Any(p => p.ParticipantID == participant.ParticipantID))
                    destination.ParticipantsProductions.Remove(participant);
            }

            foreach (var participant in source.Participants)
            {
                if (!destination.ParticipantsProductions.Any(p => p.ParticipantID == participant.ParticipantID))
                {
                    destination.ParticipantsProductions.Add(
                        new ParticipantProductionEntity
                        {
                            ParticipantID = participant.ParticipantID,
                            RoleID = participant.RoleID,
                            ProductionID = destination.ID
                        });
                }
            }

            foreach (var country in destination.Countries)
            {
                if (!source.Countries.Any(c => c == country.CountryID))
                    destination.Countries.Remove(country);
            }

            foreach(var country in source.Countries)
            {
                if (!destination.Countries.Any(c => c.CountryID == country)) { 
                    destination.Countries.Add(
                        new CountryProductionEntity
                        {
                            CountryID = country,
                            ProductionID = destination.ID
                        });
                    }
            }

            if(source is Movie)
            {
                Movie movie = source as Movie;
                destination.Movie = new MovieEntity
                {
                    LengthInMinutes = movie.LengthInMinutes
                };

                return destination;
            }

            TVShow tvShow = source as TVShow;
            destination.TVShow = new TVShowEntity
            {
                EndOfProduction = tvShow.EndOfProduction
            };

            return destination;
        }
    }
}

using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;
using System.Collections.Generic;

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
            var productionEntity = new ProductionEntity
            {
                Name = source.Name,
                Summary = source.Summary,
                YearOfCreation = source.YearOfCreation,
                ParticipantsProductions = context.Mapper.Map<List<ParticipantProductionEntity>>(source.Participants),
                Countries = context.Mapper.Map<List<CountryProductionEntity>>(source.Countries),
            };

            if(source is Movie)
            {
                Movie movie = source as Movie;
                productionEntity.Movie = new MovieEntity
                {
                    LengthInMinutes = movie.LengthInMinutes
                };

                return productionEntity;
            }

            TVShow tvShow = source as TVShow;
            productionEntity.TVShow = new TVShowEntity
            {
                EndOfProduction = tvShow.EndOfProduction
            };

            return productionEntity;
        }
    }
}

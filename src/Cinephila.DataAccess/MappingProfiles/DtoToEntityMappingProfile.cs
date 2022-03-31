using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.DTOs.ReviewDTOs;
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
                .ForMember(x => x.RoleID, opts => opts.MapFrom(x => x.RoleID))
                .ReverseMap();

            CreateMap<Movie, MovieEntity>()
                .ForMember(x => x.LengthInMinutes, opts => opts.MapFrom(x => x.LengthInMinutes))
                .ForMember(x => x.Production, opts => opts.MapFrom(x => x));

            CreateMap<TVShow, TVShowEntity>()
                .ForMember(x => x.EndOfProduction, opts => opts.MapFrom(x => x.EndOfProduction))
                .ForMember(x => x.Production, opts => opts.MapFrom(x => x));

            CreateMap<Production, ProductionEntity>().ConvertUsing<ProductionToProductionEntityResolver>();

            CreateMap<ProductionEntity, Production>().ConvertUsing<ProductionEntityToProductionResolver>();

            CreateMap<Review, ReviewProductionEntity>()
                .ForMember(x => x.Review, opts => opts.MapFrom(x => x.UserReview))
                .ReverseMap();
        }
    }

    public class ProductionToProductionEntityResolver : ITypeConverter<Production, ProductionEntity>
    {
        public ProductionEntity Convert(Production source, ProductionEntity destination, ResolutionContext context)
        {
            destination.Name = source.Name;
            destination.Summary = source.Summary;
            destination.YearOfCreation = source.YearOfCreation;
            destination.ParticipantsProductions = context.Mapper.Map<List<ParticipantProductionEntity>>(source.Participants);
            destination.Countries = context.Mapper.Map<List<CountryProductionEntity>>(source.Countries);
            
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

    public class ProductionEntityToProductionResolver : ITypeConverter<ProductionEntity, Production>
    {
        public Production Convert(ProductionEntity source, Production destination, ResolutionContext context)
        {
            if (source.Movie != null)
            {
                var movie = new Movie();
                movie.Name = source.Name;
                movie.Summary = source.Summary;
                movie.YearOfCreation = source.YearOfCreation;
                movie.Participants = context.Mapper.Map<List<ParticipantRole>>(source.ParticipantsProductions);
                movie.LengthInMinutes = source.Movie.LengthInMinutes;

                return movie;
            }

            var tvShow = new TVShow();
            tvShow.Name = source.Name;
            tvShow.Summary = source.Summary;
            tvShow.YearOfCreation = source.YearOfCreation;
            tvShow.Participants = context.Mapper.Map<List<ParticipantRole>>(source.ParticipantsProductions);

            return tvShow;
        }
    }
}

using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.FetchDataDTOs;
using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.DTOs.ReviewDTOs;
using Cinephila.Domain.DTOs.UserDTOs;
using Cinephila.Domain.Extensions;
using Cinephila.Domain.Settings;
using Microsoft.Extensions.Options;
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
                .ForMember(x => x.Runtime, opts => opts.MapFrom(x => x.LengthInMinutes))
                .ForMember(x => x.Production, opts => opts.MapFrom(x => x));

            CreateMap<TVShow, TVShowEntity>()
                .ForMember(x => x.EndOfProduction, opts => opts.MapFrom(x => x.EndOfProduction))
                .ForMember(x => x.Production, opts => opts.MapFrom(x => x));

            CreateMap<Production, ProductionEntity>().ConvertUsing<ProductionToProductionEntityResolver>();

            CreateMap<ProductionEntity, Production>().ConvertUsing<ProductionEntityToProductionResolver>();

            CreateMap<Review, ReviewProductionEntity>()
                .ForMember(x => x.Review, opts => opts.MapFrom(x => x.UserReview))
                .ReverseMap();

            CreateMap<UserInfo, UserEntity>().ReverseMap();

            // Fetch Data DTOs
            CreateMap<GenreDto, GenreEntity>()
                .ForMember(x => x.TmdbId, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.ID, opts => opts.Ignore());

            CreateMap<MovieDto, ProductionEntity>().ConvertUsing<MovieDtoToProductionEntityResolver>();

            CreateMap<PersonDto, ParticipantEntity>().ConvertUsing<PersonDtoToParticipantEntityResolver>();
        }
    }

    public class ProductionToProductionEntityResolver : ITypeConverter<Production, ProductionEntity>
    {
        public ProductionEntity Convert(Production source, ProductionEntity destination, ResolutionContext context)
        {
            destination.Name = source.Name;
            destination.Summary = source.Summary;
            destination.ReleaseDate = source.YearOfCreation;
            destination.ParticipantsProductions = context.Mapper.Map<List<ParticipantProductionEntity>>(source.Participants);
            destination.Countries = context.Mapper.Map<List<CountryProductionEntity>>(source.Countries);
            
            if(source is Movie)
            {
                Movie movie = source as Movie;
                destination.Movie = new MovieEntity
                {
                    Runtime = movie.LengthInMinutes
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
                var movie = new Movie()
                {
                    Name = source.Name,
                    Summary = source.Summary,
                    YearOfCreation = source.ReleaseDate,
                    Participants = context.Mapper.Map<List<ParticipantRole>>(source.ParticipantsProductions),
                    LengthInMinutes = source.Movie.Runtime,
                    PosterPath = source.PosterPath
                };

                return movie;
            }

            var tvShow = new TVShow()
            {
                Name = source.Name,
                Summary = source.Summary,
                YearOfCreation = source.ReleaseDate,
                Participants = context.Mapper.Map<List<ParticipantRole>>(source.ParticipantsProductions),
                PosterPath = source.PosterPath
            };

            return tvShow;
        }
    }

    public class MovieDtoToProductionEntityResolver : ITypeConverter<MovieDto, ProductionEntity>
    {
        public ProductionEntity Convert(MovieDto source, ProductionEntity destination, ResolutionContext context)
        {
            if (destination == null)
                destination = new ProductionEntity();

            destination.Name = source.Name;
            destination.Title = source.Title;
            destination.OriginalTitle = source.Original_Title;
            destination.ReleaseDate = source.Release_Date.ToNullableDateTime();
            destination.Summary = source.Overview;
            destination.TmdbID = source.Id;
            destination.PosterPath = source.Poster_Path;
            destination.Popularity = source.Popularity;
            destination.Budget = source.Budget;
            destination.Revenue = source.Revenue;
            destination.VoteAverage = source.Vote_Average;
            destination.VoteCount = source.Vote_Count;

            destination.GenresProductions = source.Genres.Select(x => new GenreProductionEntity
            {
                GenreID = x.Id,
            }).ToList();


            destination.Movie = new MovieEntity
            {
                Runtime = source.Runtime,
            };

            return destination;
        }

    }

    public class PersonDtoToParticipantEntityResolver : ITypeConverter<PersonDto, ParticipantEntity>
    {
        private ApiSettings _apiSettings { get; set; }
        public PersonDtoToParticipantEntityResolver(IOptions<ApiSettings> options)
        {
            _apiSettings = options.Value;
        }

        public ParticipantEntity Convert(PersonDto source, ParticipantEntity destination, ResolutionContext context)
        {
            if (destination == null)
                destination = new ParticipantEntity();

            destination.Name = source.Name;
            destination.TmdbId = source.Id;
            destination.Popularity = source.Popularity;
            destination.Biography = source.Biography;
            destination.BirthDate = source.BirthDay.ToNullableDateTime();
            destination.DeathDate = source.DeathDay.ToNullableDateTime();
            destination.KnownForDepartment = source.Known_For_Department;
            destination.PlaceOfBirth = source.Place_Of_Birth;

            destination.ParticipantImages = source.Images.Profiles.Select(x => new ImageEntity
            {
                Path = x.File_Path,
                VoteAverage = x.Vote_Average,
                VoteCount = x.Vote_Count,
            }).ToList();

            destination.ParticipantsProductions = source.Movie_Credits.Cast
                .DistinctBy(x => x.Id)
                .Where(x => x.Popularity > _apiSettings.MinimumPopularity && x.Adult == false)
                .Select(x => new ParticipantProductionEntity
            {
                ProductionID = x.Id,
                ParticipantID = source.Id,
                Character = x.Character,
            }).ToList();

            return destination;
        }

    }
}

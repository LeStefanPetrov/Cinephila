using AutoMapper;
using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;
using Cinephila.Domain.DTOs.ReviewDTOs;
using Cinephila.Domain.DTOs.UserDTOs;
using Cinephila.Domain.Models.ParticipantModels;
using Cinephila.Domain.Models.ProductionModels;
using Cinephila.Domain.Models.ReviewModels;
using Cinephila.Domain.Models.UserModels;

namespace Cinephila.DataAccess.MappingProfiles
{
    public class ModelToDtoMappingProfile : Profile
    {
        public ModelToDtoMappingProfile()
        {
            CreateMap<ParticipantModel, Participant>().ReverseMap();
            CreateMap<ProductionModel, Production>().ReverseMap();

            CreateMap<MovieModel, Movie>().ReverseMap();
            CreateMap<TVShowModel, TVShow>().ReverseMap();
            CreateMap<ParticipantRoleModel, ParticipantRole>().ReverseMap();

            CreateMap<ProductionCreateModel, Production>().ConvertUsing<CustomModelToProductionResolver>();

            CreateMap<ReviewModel, Review>().ReverseMap();

            CreateMap<UserInfoModel, UserInfo>().ReverseMap();
        }
    }

    public class CustomModelToProductionResolver : ITypeConverter<ProductionCreateModel, Production>
    {
        public Production Convert(ProductionCreateModel source, Production destination, ResolutionContext context)
        {
            return source.Movie != null ? context.Mapper.Map<Movie>(source.Movie) : context.Mapper.Map<TVShow>(source.TVShow);
        }
    }
}

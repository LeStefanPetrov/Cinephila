﻿using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ParticipantDTOs;
using Cinephila.Domain.DTOs.ProductionDTOs;

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

            CreateMap<TVShow, ProductionEntity>()
                .ForMember(x => x.ParticipantsProductions, opts => opts.MapFrom(x => x.Participants));

            CreateMap<Production, ProductionEntity>().ConvertUsing<ProductionToProductionEntityResolver>();
        }
    }

    public class ProductionToProductionEntityResolver : ITypeConverter<Production, ProductionEntity>
    {
        public ProductionEntity Convert(Production source, ProductionEntity destination, ResolutionContext context)
        {
            if(source is Movie)
            {
                return new ProductionEntity
                {
                    Name = source.Name,
                    Movie = context.Mapper.Map<MovieEntity>(source as Movie)
                };
            }

            return new ProductionEntity
            {
                Name = source.Name,
                TVShow = context.Mapper.Map<TVShowEntity>(source as TVShow)
            };
        }
    }
}

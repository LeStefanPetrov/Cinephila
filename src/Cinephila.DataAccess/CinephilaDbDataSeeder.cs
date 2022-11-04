using AutoMapper;
using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ApiDTOs;
using Cinephila.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Cinephila.DataAccess
{
    public static class CinephilaDbDataSeeder
    {
        public static void SeedCountries(CinephilaDbContext context)
        {
            context.Database.Migrate();

            if (!context.Countries.Any())
            {
                var countries = new List<CountryEntity>
                {
                    new CountryEntity { Name = "USA" },
                    new CountryEntity { Name = "United Kingdom" },
                    new CountryEntity { Name = "France" },
                    new CountryEntity { Name = "Italy" },
                    new CountryEntity { Name = "South Korea" },
                    new CountryEntity { Name = "Mexico" },
                    new CountryEntity { Name = "Sweden" },
                    new CountryEntity { Name = "Russia" },
                    new CountryEntity { Name = "Poland" },
                    new CountryEntity { Name = "Germany" },
                    new CountryEntity { Name = "Brazil" },
                    new CountryEntity { Name = "Canada" },
                    new CountryEntity { Name = "Japan" },
                    new CountryEntity { Name = "Australia" },
                    new CountryEntity { Name = "Spain" },
                    new CountryEntity { Name = "Austria" },
                    new CountryEntity { Name = "Bulgaria" },
                    new CountryEntity { Name = "Serbia" },
                    new CountryEntity { Name = "Turkey" },
                    new CountryEntity { Name = "Greece" },
                    new CountryEntity { Name = "Ireland"},
                };

                context.Countries.AddRange(countries);
                context.SaveChanges();
            }
        }

        public static void SeedMovies(CinephilaDbContext context, ApiSettings apiSettings)
        {
            using (var client = new HttpClient())
            {
                if (!context.Movies.Any())
                {
                    client.BaseAddress = new Uri(apiSettings.Url);
                    HttpResponseMessage response = client.GetAsync($"movie/popular?api_key={ apiSettings.Key }&page=1").Result;
                    response.EnsureSuccessStatusCode();
                    string popularMoviesResult = response.Content.ReadAsStringAsync().Result;
                    var moviesDto = JsonConvert.DeserializeObject<PopularMoviesDto>(popularMoviesResult).Movies;

                    foreach (var movie in moviesDto)
                    {
                        HttpResponseMessage creditsResponse = client.GetAsync($"movie/{ movie.ID }/credits?api_key={ apiSettings.Key }&page=1").Result;
                        response.EnsureSuccessStatusCode();
                        string creditsResult = creditsResponse.Content.ReadAsStringAsync().Result;
                        var participantsDto = JsonConvert.DeserializeObject<CreditsDto>(creditsResult).Participants;

                        var entity = new MovieEntity
                        {
                            Production = new ProductionEntity
                            {
                                ApiID = movie.ID,
                                YearOfCreation = movie.ReleaseDate,
                                Name = movie.Name,
                                PosterPath = apiSettings.PosterUrl + movie.PosterPath
                            }
                        };

                        foreach (var participant in participantsDto)
                        {
                            var participantEntity = context.Participants.FirstOrDefault(x => x.Name == participant.Name) ??
                                new ParticipantEntity { Name = participant.Name };

                            if (participantEntity.ID == 0)
                            {
                                context.Participants.Add(participantEntity);
                            }

                            context.SaveChanges();

                            if (!entity.Production.ParticipantsProductions.Any(x => x.ParticipantID == participantEntity.ID))
                            {
                                entity.Production.ParticipantsProductions.Add(new ParticipantProductionEntity
                                {
                                    ProductionID = entity.ProductionID,
                                    ParticipantID = participantEntity.ID
                                });
                            }
                        }

                        context.Movies.Add(entity);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}

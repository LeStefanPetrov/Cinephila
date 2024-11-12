using Cinephila.DataAccess.Entities;
using Cinephila.Domain.DTOs.ApiDTOs;
using Cinephila.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
                    new () { Name = "USA" },
                    new ()  { Name = "United Kingdom" },
                    new () { Name = "France" },
                    new () { Name = "Italy" },
                    new () { Name = "South Korea" },
                    new () { Name = "Mexico" },
                    new () { Name = "Sweden" },
                    new () { Name = "Russia" },
                    new () { Name = "Poland" },
                    new () { Name = "Germany" },
                    new () { Name = "Brazil" },
                    new () { Name = "Canada" },
                    new () { Name = "Japan" },
                    new () { Name = "Australia" },
                    new () { Name = "Spain" },
                    new () { Name = "Austria" },
                    new () { Name = "Bulgaria" },
                    new () { Name = "Serbia" },
                    new () { Name = "Turkey" },
                    new () { Name = "Greece" },
                    new () { Name = "Ireland"},
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

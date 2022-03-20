using Cinephila.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cinephila.DataAccess
{
    public static class CinephilaDbDataSeeder
    {
        public static void SeedCountries(CinephilaDbContext context)
        {
            context.Database.Migrate();

            if(!context.Countries.Any())
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
    }
}

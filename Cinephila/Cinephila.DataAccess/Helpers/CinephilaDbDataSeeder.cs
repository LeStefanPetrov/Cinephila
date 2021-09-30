using Cinephila.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cinephila.DataAccess.Helpers
{
    public static class CinephilaDbDataSeeder
    {
        public static void SeedCountries(CinephilaDbContext context)
        {
            context.Database.Migrate();

            if(!context.Countries.Any())
            {
                var countries = new List<Country>
                {
                    new Country { Name = "USA" },
                    new Country { Name = "United Kingdom" },
                    new Country { Name = "France" },
                    new Country { Name = "Italy" },
                    new Country { Name = "South Korea" },
                    new Country { Name = "Mexico" },
                    new Country { Name = "Sweden" },
                    new Country { Name = "Russia" },
                    new Country { Name = "Poland" },
                    new Country { Name = "Germany" },
                    new Country { Name = "Brazil" },
                    new Country { Name = "Canada" },
                    new Country { Name = "Japan" },
                    new Country { Name = "Australia" },
                    new Country { Name = "Spain" },
                    new Country { Name = "Austria" },
                    new Country { Name = "Bulgaria" },
                    new Country { Name = "Serbia" },
                    new Country { Name = "Turkey" },
                    new Country { Name = "Greece" },
                    new Country { Name = "Ireland"},
                };

                context.Countries.AddRange(countries);
                context.SaveChanges();
            }
        }
    }
}

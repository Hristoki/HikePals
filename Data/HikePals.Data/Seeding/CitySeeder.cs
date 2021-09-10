namespace HikePals.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CitySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cities.Any())
            {
                return;
            }

            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Sofia" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Varna" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Plovdiv" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Pazardzhik" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Vratsa" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Ruse" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Blagoevgrad" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Burgas" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Veliko Tarnovo" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Vidin" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Razgrad" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Pernik" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Borovets" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Karlovo" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Samokov" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Asenovgrad" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Berkovitsa" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Kalofer" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Svoge" });
            await dbContext.Cities.AddAsync(new Models.City { CountryId = 1, Name = "Troyan" });
        }
    }
}

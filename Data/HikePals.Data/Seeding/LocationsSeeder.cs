namespace HikePals.Data.Seeding
{
    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class LocationsSeeder : ISeeder
    {

        public LocationsSeeder()
        {

        }
       
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var categoryRepository = serviceProvider.GetRequiredService<IDeletableEntityRepository<Category>>();
            var cityRepository = serviceProvider.GetRequiredService<IDeletableEntityRepository<City>>();

            if (dbContext.Locations.Any())
            {
                return;
            }
           
            await dbContext.Locations.AddAsync
                (new Location { Name = "Cherni Vrah", Category = GetCategory(dbContext, "Summit"), CityId = GetCity(dbContext, "Sofia") });

            await dbContext.Locations.AddAsync
                (new Location { Name = "Musala", Category = GetCategory(dbContext, "Summit"), CityId = GetCity(dbContext, "Borovets"), });

            await dbContext.Locations.AddAsync
                (new Location { Name = "Malyovitsa", Category = GetCategory(dbContext, "Summit"), CityId = GetCity(dbContext, "Samokov"), });

            await dbContext.Locations.AddAsync
                (new Location { Name = "Assen's Fortress", Category = GetCategory(dbContext, "Fortress"), CityId = GetCity(dbContext, "Asenovgrad"), });

            await dbContext.Locations.AddAsync
                (new Location { Name = "Raisko praskalo", Category = GetCategory(dbContext, "Waterfall"), CityId = GetCity(dbContext, "Kalofer"), });

            await dbContext.Locations.AddAsync
                (new Location { Name = "Pod Kamiko", Category = GetCategory(dbContext, "Eco trail"), CityId = GetCity(dbContext, "Svoge"), });

            await dbContext.Locations.AddAsync
                (new Location { Name = "Rila Monastery", Category = GetCategory(dbContext, "Monastery"), CityId = GetCity(dbContext, "Blagoevgrad"), });

            await dbContext.Locations.AddAsync
                (new Location { Name = "Ambaritsa Chattel", Category = GetCategory(dbContext, "Chattel"), CityId = GetCity(dbContext, "Troyan"), });

            await dbContext.Locations.AddAsync
                (new Location { Name = "Byala reka", Category = GetCategory(dbContext, "Eco trail"), CityId = GetCity(dbContext, "Karlovo"), });
        }

        private static int GetCity(ApplicationDbContext db, string city)
        {
            var cityId = db.Cities.FirstOrDefault(x => x.Name == city).Id;
            return cityId;
        }

        private static Category GetCategory(ApplicationDbContext dbContext, string categoryName)
        {
           return dbContext.Categories.FirstOrDefault(x => x.Name == categoryName);
        }

    }
}

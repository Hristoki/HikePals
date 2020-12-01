using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikePals.Data.Seeding
{
    public class LocationCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.LocationCategories.Any())
            {
                return;
            }

            await dbContext.LocationCategories.AddAsync(new Models.LocationCategory { Name = "Waterfall" });
            await dbContext.LocationCategories.AddAsync(new Models.LocationCategory { Name = "Cave" });
            await dbContext.LocationCategories.AddAsync(new Models.LocationCategory { Name = "Peak" });
            await dbContext.LocationCategories.AddAsync(new Models.LocationCategory { Name = "Village" });
            await dbContext.LocationCategories.AddAsync(new Models.LocationCategory { Name = "Fortress" });
            await dbContext.LocationCategories.AddAsync(new Models.LocationCategory { Name = "Eco trail" });
            await dbContext.LocationCategories.AddAsync(new Models.LocationCategory { Name = "Chattel" });
            await dbContext.LocationCategories.AddAsync(new Models.LocationCategory { Name = "Lake" });
            await dbContext.LocationCategories.AddAsync(new Models.LocationCategory { Name = "River" });
            await dbContext.LocationCategories.AddAsync(new Models.LocationCategory { Name = "Monastery" });
        }
    }
}

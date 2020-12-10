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

            await dbContext.LocationCategories.AddAsync(new Models.Category { Name = "Waterfall" });
            await dbContext.LocationCategories.AddAsync(new Models.Category { Name = "Cave" });
            await dbContext.LocationCategories.AddAsync(new Models.Category { Name = "Peak" });
            await dbContext.LocationCategories.AddAsync(new Models.Category { Name = "Village" });
            await dbContext.LocationCategories.AddAsync(new Models.Category { Name = "Fortress" });
            await dbContext.LocationCategories.AddAsync(new Models.Category { Name = "Eco trail" });
            await dbContext.LocationCategories.AddAsync(new Models.Category { Name = "Chattel" });
            await dbContext.LocationCategories.AddAsync(new Models.Category { Name = "Lake" });
            await dbContext.LocationCategories.AddAsync(new Models.Category { Name = "River" });
            await dbContext.LocationCategories.AddAsync(new Models.Category { Name = "Monastery" });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikePals.Data.Seeding
{
    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Models.Category { Name = "Waterfall" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Cave" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Summit" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Village" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Fortress" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Eco trail" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Chattel" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Lake" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "River" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Monastery" });
        }
    }
}

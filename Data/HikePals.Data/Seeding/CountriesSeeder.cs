namespace HikePals.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CountriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Countries.Any())
            {
                return;
            }

            await dbContext.Countries.AddAsync(new Models.Country { Name = "Bulgaria" });
            await dbContext.Countries.AddAsync(new Models.Country { Name = "France" });
            await dbContext.Countries.AddAsync(new Models.Country { Name = "Romania" });
            await dbContext.Countries.AddAsync(new Models.Country { Name = "Greece" });
            await dbContext.Countries.AddAsync(new Models.Country { Name = "Italy" });
        }
    }
}

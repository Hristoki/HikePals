namespace HikePals.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TransportsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Transports.Any())
            {
                return;
            }

            await dbContext.Transports.AddAsync(new Models.Transport { Name = "Car" });
            await dbContext.Transports.AddAsync(new Models.Transport { Name = "Bus" });
            await dbContext.Transports.AddAsync(new Models.Transport { Name = "Plane" });
            await dbContext.Transports.AddAsync(new Models.Transport { Name = "Train" });
            await dbContext.Transports.AddAsync(new Models.Transport { Name = "Taxi" });
            await dbContext.Transports.AddAsync(new Models.Transport { Name = "Boat" });
            await dbContext.Transports.AddAsync(new Models.Transport { Name = "Bycycle" });
            await dbContext.Transports.AddAsync(new Models.Transport { Name = "Motоrcycle" });
            await dbContext.Transports.AddAsync(new Models.Transport { Name = "No transport needed" });
        }
    }
}

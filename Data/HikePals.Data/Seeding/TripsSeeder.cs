namespace HikePals.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HikePals.Data.Models;

    public class TripsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Trips.Any())
            {
                return;
            }

            var trips = new List<Trip>();
            var user = this.GetUser(dbContext, "testUser1@gmail.com");

            await dbContext.AddAsync(new Trip
            {
                Title = "From Golden Bridges to Cherni Vrah",
                Description = "We started our journey on a beautiful Saturday morning. Our beginning point were the beautiful Golden Bridges. We were there by 9:30 (don’t judge, it was a Saturday). Basically, the route goes as follows: Golden Bridges – Kumata Hut: 45 minutes; Kumata Hut – Cherni Vrah: 2.5 hours.",
                Distance = 20,
                Duration = new TimeSpan(3, 15, 0),
                CreatedByUser = dbContext.Users.FirstOrDefault(),
                LocationId = this.GetLocation(dbContext, "Cherni Vrah"),
                Image = new Image { Id = "1", User = user, Extentsion = ".jpeg" },
                CreatedByUserId = user.Id,
            });

            await dbContext.Trips.AddRangeAsync();
        }

        private int GetLocation(ApplicationDbContext db, string name)
        {
            return db.Locations.FirstOrDefault(x => x.Name == name).Id;
        }

        private ApplicationUser GetUser(ApplicationDbContext db, string userName)
        {
            return db.Users.FirstOrDefault(x => x.Name == userName);
        }
    }
}

namespace HikePals.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HikePals.Common;
    using HikePals.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (dbContext.Users.Any())
            {
                return;
            }

            await SeedUserAsync(userManager, "admin@gmail.com");
            for (int i = 1; i < 20; i++)
            {
                await SeedUserAsync(userManager, $"testUser{i}");
            }
            await SeedUserAsync(userManager, "hristo@gmail.com");
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string username)
        {
            var user = await userManager.FindByNameAsync(username);
            IdentityResult result = new IdentityResult();

            if (user == null && username == "admin@gmail.com")
            {
                var appUser = new ApplicationUser
                {
                    UserName = username,
                    Email = username,
                    DateOfBirth = DateTime.UtcNow,
                    CityId = 2,
                    Name = "admin",
                };

                var password = "admin";

                result = userManager.CreateAsync(appUser, password).Result;

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }
            }
            else if (user == null && username.StartsWith("test"))
            {
                var appUser = new ApplicationUser
                {
                    UserName = username,
                    Email = username,
                    DateOfBirth = DateTime.UtcNow,
                    CityId = 2,
                    Name = username,
                };

                var password = username;

                result = userManager.CreateAsync(appUser, password).Result;

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
                }
            }
        }
    }
}

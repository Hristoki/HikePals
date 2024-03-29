﻿namespace HikePals.Data.Seeding
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

            await SeedUserAsync(userManager, "admin@abv.com");

            for (int i = 1; i < 20; i++)
            {
                await SeedUserAsync(userManager, $"testUser{i}");
            }
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string username)
        {
            ApplicationUser user;
            IdentityResult result = new IdentityResult();

            if (username == "admin@abv.bg")
            {
                user = new ApplicationUser
                {
                    UserName = username,
                    Email = "admin@abv.bg",
                    DateOfBirth = DateTime.UtcNow,
                    CityId = 2,
                    Name = "admin@abv.bg",
                };

                var password = "admin@abv.com";

                result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }
            }
            else if (username.StartsWith("test"))
            {
                user = new ApplicationUser
                {
                    UserName = $"{username}@gmail.com",
                    Email = $"{username}@gmail.com",
                    DateOfBirth = DateTime.UtcNow,
                    CityId = 2,
                    Name = $"{username}@gmail.com",
                };

                var password = "123456";
                result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
                }
            }
        }
    }
}

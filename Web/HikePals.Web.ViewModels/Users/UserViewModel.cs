using HikePals.Data.Models;
using HikePals.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace HikePals.Web.ViewModels.Users
{
   public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Name { get; set; }

        public string CityName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}

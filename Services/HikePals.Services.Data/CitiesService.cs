namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class CitiesService : ICitiesService
    {
        private readonly IRepository<City> cityRepository;

        public CitiesService(IRepository<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public IEnumerable<SelectListItem> GetAllCities()
        {
            var result = this.cityRepository
                .AllAsNoTracking()
                .Select(x => new {x.Name, x.Id })
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            return result.OrderBy(x => x.Text);
        }
    }
}

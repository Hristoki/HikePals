using HikePals.Data.Common.Repositories;
using HikePals.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HikePals.Services.Data
{
    public class CitiesService : ICitiesService
    {
        private readonly IDeletableEntityRepository<City> cityRepository;

        public CitiesService(IDeletableEntityRepository<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }
        public IEnumerable<SelectListItem> GetAllCities()
        {
            return cityRepository.All().Select(x => new {x.Name, x.Id }).Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
        }
    }
}

using HikePals.Data.Common.Repositories;
using HikePals.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HikePals.Services.Data
{
    public class CountriesService : ICountryService
    {
        private readonly IDeletableEntityRepository<Country> countryRepository;

        public CountriesService(IDeletableEntityRepository<Country> countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public IEnumerable<SelectListItem> GetAllCountries()
        {
            return countryRepository.AllAsNoTracking()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }
    }
}

namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CountriesService : ICountriesService
    {
        private readonly IRepository<Country> countryRepository;

        public CountriesService(IRepository<Country> countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public IEnumerable<SelectListItem> GetAllCountries()
        {
            return this.countryRepository.AllAsNoTracking()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }
    }
}

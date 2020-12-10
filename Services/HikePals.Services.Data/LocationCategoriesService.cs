using HikePals.Data.Common.Repositories;
using HikePals.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HikePals.Services.Data
{
    public class LocationCategoriesService : ILocationCategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public LocationCategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }


        public IEnumerable<SelectListItem> GetAllLocationCategories()
        {
          return this.categoriesRepository
                .AllAsNoTracking()
                .Select(x => new { x.Id, x.Name })
                .ToList()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }
    }
}

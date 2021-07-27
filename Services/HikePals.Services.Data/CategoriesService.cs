namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using HikePals.Services.Data.Contracts;
    using HikePals.Services.Mapping;
    using HikePals.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesRepository;
        private readonly IRepository<Trip> tripRepo;

        public CategoriesService(IRepository<Category> categoriesRepo, IRepository<Trip> tripRepo)
        {
            this.categoriesRepository = categoriesRepo;
            this.tripRepo = tripRepo;
        }

        public IEnumerable<SingleCategoryViewModel> GetAllCategories()
        {
            var categories = this.categoriesRepository.All().To<SingleCategoryViewModel>().OrderBy(x => x.Name).ToList();
            foreach (var category in categories)
            {
                //this.locationsRepo.All().Where(x => x.CategoryId == category.Id).Select(x => x.Trips.Count).FirstOrDefault();
                var tripCount = this.tripRepo.All().Where(x => x.Location.CategoryId == category.Id).Count();
                category.TripsCount = tripCount;

            }

            return categories;

        }

        public IEnumerable<SelectListItem> GetAllCategoriesListItems()
        {
          var result = this.categoriesRepository
                .AllAsNoTracking()
                .Select(x => new { x.Id, x.Name })
                .ToList()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

          return result.OrderBy(x => x.Text);
        }

    }
}

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

        public CategoriesService(IRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<SingleCategoryViewModel> GetAllCategories()
        {
            return this.categoriesRepository.All().To<SingleCategoryViewModel>().OrderBy(x => x.Name).ToList();
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

namespace HikePals.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HikePals.Services.Data.Contracts;
    using HikePals.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CategoriesController : BaseController
    {
        private ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var model = new AllCategoriesViewModel()
            {
                Categories = this.categoriesService.GetAllCategories(),
            };
            return this.View(model);
        }
    }
}

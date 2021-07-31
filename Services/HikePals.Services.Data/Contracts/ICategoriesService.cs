namespace HikePals.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HikePals.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
        IEnumerable<SelectListItem> GetAllCategoriesAsListItems();

        IEnumerable<SingleCategoryViewModel> GetAllCategories();

        List<string> All();
    }
}

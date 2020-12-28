namespace HikePals.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllCategoriesViewModel
    {
        public IEnumerable<SingleCategoryViewModel> Categories { get; set; }
    }
}

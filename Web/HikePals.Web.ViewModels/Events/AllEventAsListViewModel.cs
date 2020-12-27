namespace HikePals.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllEventAsListViewModel
    {
        public IEnumerable<SingleEventListViewModel> Events { get; set; }
    }
}

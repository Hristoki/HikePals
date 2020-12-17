namespace HikePals.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllEventAsListViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }
    }
}

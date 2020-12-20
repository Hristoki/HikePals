namespace HikePals.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AllEventsAdminViewModel
    {
        public IEnumerable<HikePals.Web.ViewModels.Events.AdminEventViewModel> Events { get; set; }

        public string Title => "Events Overview";
    }
}

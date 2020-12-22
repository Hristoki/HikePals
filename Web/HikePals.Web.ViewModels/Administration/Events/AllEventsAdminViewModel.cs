namespace HikePals.Web.ViewModels.Administration.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AllEventsAdminViewModel
    {
        public IEnumerable<HikePals.Web.ViewModels.Administration.Users.SingleUserAdminViewModel> Events { get; set; }

        public string Title => "Events Overview";
    }
}

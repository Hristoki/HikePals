namespace HikePals.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditEventViewModel : BaseEventViewModel, IMapFrom<Event>
    {
        public int TransportId { get; set; }

        public IEnumerable<SelectListItem> TransportItems { get; set; }
    }
}

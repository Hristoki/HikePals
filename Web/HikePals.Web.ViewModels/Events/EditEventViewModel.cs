namespace HikePals.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditEventViewModel : CreateEventInputViewModel, IMapFrom<Event>, IHaveCustomMappings
    {
        public string TransportName { get; set; }

        public int Id { get; set; }
    }
}

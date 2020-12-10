namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditTripViewModel : BaseTripViewModel
    {

        public int Id { get; set; }

        [Required]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> CityItems { get; set; }

        [Required]
        public int TypeOfDestinationId { get; set; }

        public IEnumerable<SelectListItem> CategoriesItems { get; set; }

    }
}

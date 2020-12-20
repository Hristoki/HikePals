namespace HikePals.Web.ViewModels.Rating
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class RatingInputModel
    {
        [Required]
        [Range(1, 5)]
        public byte Value { get; set; }

        [Required]
        public int TripId { get; set; }
    }
}

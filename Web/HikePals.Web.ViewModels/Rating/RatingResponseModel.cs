namespace HikePals.Web.ViewModels.Rating
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class RatingResponseModel
    {
        [Required]
        [Range(1, 5)]
        public byte UserVoteValue { get; set; }

        [Required]
        public double AverageRating { get; set; }
    }
}

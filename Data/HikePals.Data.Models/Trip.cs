namespace HikePals.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using HikePals.Data.Common.Models;

    public class Trip : BaseDeletableModel<int>
    {
        public Trip()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Tags = new HashSet<TripsTags>();
        }

        public string TripName { get; set; }

        // [Required]
        public int DestinationId { get; set; }

        public Location Destination { get; set; }

        // [Required]
        // [MaxLength(500)]
        public string Description { get; set; }

        public string CreatedByUserId { get; set; }

        // [Required]
        public virtual ApplicationUser CreatedByUser { get; set; }

        public int Length { get; set; }

        public TimeSpan ApproximateDuration { get; set; }

        [ForeignKey("TripImage")]
        public string TripImageId { get; set; }

        public virtual TripImage TripImage { get; set; }

        public virtual ICollection<TripsTags> Tags { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }
    }
}

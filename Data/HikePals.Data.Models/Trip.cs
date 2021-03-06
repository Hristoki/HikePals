﻿namespace HikePals.Data.Models
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
            this.Events = new HashSet<Event>();
        }

        public string Title { get; set; }

        // [Required]
        public int LocationId { get; set; }

        public Location Location { get; set; }

        // [Required]
        // [MaxLength(500)]
        public string Description { get; set; }

        public string CreatedByUserId { get; set; }

        // [Required]
        public virtual ApplicationUser CreatedByUser { get; set; }

        public int Distance { get; set; }

        public TimeSpan Duration { get; set; }

        [ForeignKey("Image")]
        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<TripsTags> Tags { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}

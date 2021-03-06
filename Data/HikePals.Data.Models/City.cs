﻿namespace HikePals.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HikePals.Data.Common.Models;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            this.Locations = new HashSet<Location>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
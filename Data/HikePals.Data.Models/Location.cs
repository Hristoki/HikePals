namespace HikePals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using HikePals.Data.Common.Models;

    public class Location: BaseDeletableModel<int>
    {
        public Location()
        {
        }

        public string Name { get; set; }

        [ForeignKey("LocationCategory")]
        public int CategoryId { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string Description { get; set; }
    }
}

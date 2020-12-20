using HikePals.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace HikePals.Data.Models
{
    public class Rating : BaseDeletableModel<int>
    {
        [Required]
        public byte Value { get; set; }

        public string UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        public int TripId { get; set; }

        [Required]
        public virtual Trip Trip { get; set; }

    }
}
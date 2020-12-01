using System.ComponentModel.DataAnnotations;

namespace HikePals.Data.Models
{
    public class Rating
    {
       // [Required]
        public byte Rate { get; set; }

        public int Id { get; set; }

        public int UserId { get; set; }

        //[Required]
        public ApplicationUser User { get; set; }

        public int TripId { get; set; }

      //  [Required]
        public Trip Trip { get; set; }

    }
}
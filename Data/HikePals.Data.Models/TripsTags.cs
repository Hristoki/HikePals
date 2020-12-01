namespace HikePals.Data.Models
{
    public class TripsTags
    {
        public string TripId { get; set; }

        public Trip Trip { get; set; }

        public string TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
namespace HikePals.Data.Models
{
    using System;

    using HikePals.Data.Common.Models;

    public class Image: BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Url { get; set; }

        public string Extentsion { get; set; }
    }
}

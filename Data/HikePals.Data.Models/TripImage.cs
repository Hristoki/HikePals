using HikePals.Data.Common.Models;
using System;

namespace HikePals.Data.Models
{
    public class TripImage: BaseDeletableModel<string>
    {
        public TripImage()
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

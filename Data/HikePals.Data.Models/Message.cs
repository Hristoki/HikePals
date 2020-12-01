using HikePals.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HikePals.Data.Models
{
    public class Message: BaseDeletableModel<string>
    {

        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Content { get; set; }

        [ForeignKey("ApplicationUser")]

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser SentBy { get; set; }

        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

    }
}
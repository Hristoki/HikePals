namespace HikePals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using HikePals.Data.Common.Models;

    public class Message: BaseDeletableModel<string>
    {

        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Content { get; set; }

        public DateTime TimeStamp { get; set; }

        [ForeignKey("ApplicationUser")]

        public string SentById { get; set; }

        public virtual ApplicationUser SentBy { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }

    }
}
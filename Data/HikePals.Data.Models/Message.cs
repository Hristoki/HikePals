namespace HikePals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using HikePals.Data.Common.Models;

    public class Message : BaseDeletableModel<string>
    {

        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Content { get; set; }

        public string SentById { get; set; }

        public ApplicationUser SentBy { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

    }
}
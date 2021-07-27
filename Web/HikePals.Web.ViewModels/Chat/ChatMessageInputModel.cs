namespace HikePals.Web.ViewModels.Chat
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public class ChatMessageInputModel : IMapTo<Message>
    {
        public int EventId { get; set; }

        public string SentById { get; set; }

        public string Content { get; set; }

    }
}

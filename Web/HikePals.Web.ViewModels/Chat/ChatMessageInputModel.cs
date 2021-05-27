namespace HikePals.Web.ViewModels.Chat
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ChatMessageInputModel
    {
        public DateTime TimeInUtc { get; set; }

        public int EventId { get; set; }

        public string SendById { get; set; }

        public string SendByName { get; set; }

        public string Text { get; set; }

    }
}

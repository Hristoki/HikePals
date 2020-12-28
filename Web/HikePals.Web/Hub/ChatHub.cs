namespace HikePals.Web.Hub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HikePals.Web.ViewModels.Chat;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

    public class ChatHub : Hub
    {
        public ChatHub()
        {

        }

        public async Task Send(string message)
        {

            var username = this.Context.User.Identity.Name;
            var contextItems = this.Context.Items;
            var timeInUtc = DateTime.UtcNow;
            var msgLogTime = DateTime.UtcNow.ToString("g");

            await this.Clients.All.SendAsync(
                "NewMessage",
                new MessageResponseModel { SendByName = username, Text = message, Time = msgLogTime });

        }
    }
}

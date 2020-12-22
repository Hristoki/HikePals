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
        public async Task Send(string message)
        {
            var username = this.Context.User.Identity.Name;
            var msgLogTime = DateTime.UtcNow.ToString();

            await this.Clients.All.SendAsync(
                "NewMessage",
                new MessageResponseModel { ApplicationUserName = username, Text = message, Time = msgLogTime});
        }
    }
}

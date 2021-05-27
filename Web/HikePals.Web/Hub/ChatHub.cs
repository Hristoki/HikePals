namespace HikePals.Web.Hub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HikePals.Services.Data.Contracts;
    using HikePals.Web.ViewModels.Chat;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

    public class ChatHub : Hub
    {
        private readonly IChatService chatService;

        public ChatHub(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public async Task Send(ChatMessageInputModel input)
        {
            //var contextItems = this.Context.Items;

            var username = this.Context.User.Identity.Name;
            var userId = this.Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var timeInUtc = DateTime.UtcNow;
            var msgLogTime = timeInUtc.ToString("g");

            var message = new ChatMessageInputModel() { SendById = userId, Text = input.Text, EventId = input.EventId };

            await this.chatService.SaveMessageAsync(message);

            //var users = new string[] { "asd", "asd1" };

            var a = 5;

            await this.Clients.All.SendAsync(
                "NewMessage",
                new MessageResponseModel { SendByName = username, Text = input.Text, Time = msgLogTime });

        }
    }
}

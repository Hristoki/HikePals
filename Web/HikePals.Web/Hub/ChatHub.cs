namespace HikePals.Web.Hub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HikePals.Data.Models;
    using HikePals.Services.Data.Contracts;
    using HikePals.Web.ViewModels.Chat;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

    public class ChatHub : Hub
    {
        private readonly IChatService chatService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChatHub(IChatService chatService, UserManager<ApplicationUser> userManager)
        {
            this.chatService = chatService;
            this.userManager = userManager;
        }

        public async Task Send(ChatMessageInputModel input)
        {
            //var username = this.Context.User.Identity.Name;

            //var userId = this.Context.User.FindFirst(ClaimTypes.NameIdentifier).Type;
            //var input = new ChatMessageInputModel() { Content = content, EventId = eventId, SentById = userId };

            var user = this.userManager.GetUserAsync(this.Context.User).Result;
            input.SentById = user.Id;
            var msgLogTime = DateTime.UtcNow;
            var localTime = msgLogTime.ToLocalTime().ToShortTimeString();
            var username = user.Name;

            await this.chatService.SaveMessageAsync(input);

            await this.Clients.All.SendAsync(
                    "NewMessage",
                    new MessageResponseModel { SendByName = username, Text = input.Content, Time = localTime });
        }
    }
}

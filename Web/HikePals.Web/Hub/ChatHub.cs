namespace HikePals.Web.Hub
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HikePals.Data.Models;
    using HikePals.Services.Data;
    using HikePals.Services.Data.Contracts;
    using HikePals.Web.ViewModels.Chat;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        private readonly IChatService chatService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEventsService eventsService;

        public ChatHub(IChatService chatService, UserManager<ApplicationUser> userManager, IEventsService eventsService)
        {
            this.chatService = chatService;
            this.userManager = userManager;
            this.eventsService = eventsService;
        }

        public async Task Send(ChatMessageInputModel input)
        {
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

        public async Task JoinGroup(int eventId)
        {
            var userId = this.Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (this.eventsService.UserHasJoinedEvent(eventId, userId))
            {
             await this.Groups.AddToGroupAsync(this.Context.ConnectionId, eventId.ToString());
            }

            return;
        }
    }
}

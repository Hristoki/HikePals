namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using HikePals.Services.Data.Contracts;
    using HikePals.Web.ViewModels.Chat;

    public class ChatService : IChatService
    {
        private readonly IDeletableEntityRepository<Message> messageRepository;

        public ChatService(IDeletableEntityRepository<Message> messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public IEnumerable<MessageResponseModel> GetChatHistory()
        {
            return null;
        }

        public async Task SaveMessageAsync(ChatMessageInputModel message)
        {
            var msg = new Message
            {
                Content = message.Text,
                SentById = message.SendById,
                TimeStamp = message.TimeInUtc,
                EventId = message.EventId,
            };

            await this.messageRepository.AddAsync(msg);
            await this.messageRepository.SaveChangesAsync();
        }
    }
}

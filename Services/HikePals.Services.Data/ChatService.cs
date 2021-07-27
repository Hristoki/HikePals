namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using HikePals.Services.Data.Contracts;
    using HikePals.Web.ViewModels.Chat;

    public class ChatService : IChatService
    {
        private readonly IDeletableEntityRepository<Message> messageRepository;
        private readonly IMapper mapper;

        public ChatService(IDeletableEntityRepository<Message> messageRepository, IMapper mapper)
        {
            this.messageRepository = messageRepository;
            this.mapper = mapper;
        }

        public IEnumerable<MessageResponseModel> GetChatHistory()
        {
            return null;
        }

        public async Task SaveMessageAsync(ChatMessageInputModel input)
        {
            var message = this.mapper.Map<Message>(input);

            await this.messageRepository.AddAsync(message);
            await this.messageRepository.SaveChangesAsync();
        }
    }
}

namespace HikePals.Web.Infrastructure.Profiles
{
    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Web.ViewModels.Chat;

    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
        this.CreateMap<Message, ChatMessageInputModel>().ReverseMap();
        this.CreateMap<Message, MessageResponseModel>().ReverseMap();
        }
    }
}

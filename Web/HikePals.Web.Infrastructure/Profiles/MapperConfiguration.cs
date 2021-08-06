namespace HikePals.Web.Infrastructure.Profiles
{
    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Web.ViewModels.Chat;

    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
        this.CreateMap<Message, ChatMessageInputModel>().ReverseMap();
        this.CreateMap<Message, MessageResponseModel>().ReverseMap();
        }
    }
}

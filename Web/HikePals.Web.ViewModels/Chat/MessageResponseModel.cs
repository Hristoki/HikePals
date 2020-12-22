namespace HikePals.Web.ViewModels.Chat
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public class MessageResponseModel : IMapFrom<Message>, IMapTo<Message>, IHaveCustomMappings
    {

        public string Time { get; set; }

        public string ApplicationUserId { get; set; }

        public string ApplicationUserName { get; set; }

        public string Text { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, MessageResponseModel>()
                .ForMember(t => t.ApplicationUserName, s => s.MapFrom(x => x.SentBy.Name == null ? x.SentBy.UserName : x.SentBy.Name))
                .ForMember(t => t.ApplicationUserId, s => s.MapFrom(x => x.ApplicationUserId))
                .ForMember(t => t.Text, s => s.MapFrom(x => x.Content));
        }
    }
}

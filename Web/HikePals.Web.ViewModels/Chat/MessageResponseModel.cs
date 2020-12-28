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

        public DateTime TimeInUtc { get; set; }

        public int EventId { get; set; }

        public string SendById { get; set; }

        public string SendByName { get; set; }

        public string Text { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, MessageResponseModel>()
                .ForMember(t => t.SendByName, s => s.MapFrom(x => x.SentBy.Name == null ? x.SentBy.UserName : x.SentBy.Name))
                .ForMember(t => t.SendById, s => s.MapFrom(x => x.SentById))
                .ForMember(t => t.Text, s => s.MapFrom(x => x.Content));
        }
    }
}

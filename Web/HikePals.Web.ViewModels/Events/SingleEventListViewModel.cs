namespace HikePals.Web.ViewModels.Events
{
    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public class SingleEventListViewModel : BaseEventViewModel, IMapFrom<Event>, IHaveCustomMappings
    {
        public double AverageRating { get; set; }

        public int CurrentNumberParticipants { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, SingleEventListViewModel>()
                .ForMember(t => t.CurrentNumberParticipants, s => s.MapFrom(x => x.Participants.Count))
                 .ForMember(t => t.ImageUrl, s => s.MapFrom(x => "/images/trips/" + x.Trip.Image.Id + x.Trip.Image.Extentsion));
        }
    }
}

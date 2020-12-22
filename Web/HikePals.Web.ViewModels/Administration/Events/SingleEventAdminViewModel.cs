namespace HikePals.Web.ViewModels.Administration.Events
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using HikePals.Web.ViewModels.Events;

    public class SingleEventAdminViewModel : BaseEventViewModel, IMapFrom<Event>, IHaveCustomMappings
    {
        public bool IsDeleted { get; set; }

        public string DeletedOn { get; set; }

        public string CreatedOn { get; set; }

        public string ModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, SingleEventAdminViewModel>()
                       .ForMember(t => t.Image, s => s.MapFrom(x => "/images/trips/" + x.Trip.Image.Id + x.Trip.Image.Extentsion))
                       .ForMember(t => t.CreatedBy, s => s.MapFrom(x => x.CreatedBy.Name))
                       .ForMember(t => t.CreatedOn, s => s.MapFrom(x => x.CreatedOn.ToString("g", CultureInfo.InvariantCulture)))
                       .ForMember(t => t.DeletedOn, s => s.MapFrom(x => x.DeletedOn == null ? "N / A" : x.DeletedOn.Value.ToString("g", CultureInfo.InvariantCulture)))
                       .ForMember(t => t.ModifiedOn, s => s.MapFrom(x => x.ModifiedOn == null ? "N / A" : x.ModifiedOn.Value.ToString("g", CultureInfo.InvariantCulture)))
                       .ForMember(t => t.IsDeleted, s => s.MapFrom(x => x.IsDeleted));
        }
    }

}

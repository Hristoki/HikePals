namespace HikePals.Web.ViewModels.Administration.Trips
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using HikePals.Web.ViewModels.Trips;

    public class SingleTripViewModel : BaseTripViewModel, IMapFrom<Trip>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string DeletedOn { get; set; }

        public string CreatedOn { get; set; }

        public string ModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Trip, SingleTripViewModel>()
                .ForMember(t => t.ImageUrl, s =>
                    s.MapFrom(x =>
                        x.Image == null ? "No image available" : "/images/trips/" + x.Image.Id + x.Image.Extentsion))
                                 .ForMember(t => t.CreatedBy, s => s.MapFrom(x => x.CreatedByUser.Name))
                       .ForMember(t => t.CreatedOn, s => s.MapFrom(x => x.CreatedOn.ToString("g", CultureInfo.InvariantCulture)))
                       .ForMember(t => t.DeletedOn, s => s.MapFrom(x => x.DeletedOn == null ? "N / A" : x.DeletedOn.Value.ToString("g", CultureInfo.InvariantCulture)))
                       .ForMember(t => t.ModifiedOn, s => s.MapFrom(x => x.ModifiedOn == null ? "N / A" : x.ModifiedOn.Value.ToString("g", CultureInfo.InvariantCulture)))
                       .ForMember(t => t.IsDeleted, s => s.MapFrom(x => x.IsDeleted));
        }
    }
}

namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public abstract class BaseTripViewModel : IMapFrom<Trip>, IHaveCustomMappings
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string LocationCategoryName { get; set; }

        public string LocationName { get; set; }

        public double AverageRating { get; set; }

        public string CreatedById { get; set; }

        public string UserId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Trip, TripViewModel>()
             .ForMember(t => t.ImageUrl, s =>
                 s.MapFrom(x => x.Image == null ? "No image available" : "/images/trips/" + x.Image.Id + x.Image.Extentsion))
             .ForMember(x => x.AverageRating, t => t.MapFrom(y => y.Rating.Count() == 0 ? 0 : y.Rating.Average(z => z.Value)))
            .ForMember(x => x.CreatedById, t => t.MapFrom(y => y.CreatedByUserId));

            configuration.CreateMap<Trip, SingleTripListViewModel>()
             .ForMember(x => x.AverageRating, t => t.MapFrom(y => y.Rating.Count() == 0 ? 0 : y.Rating.Average(z => z.Value)))
             .ForMember(t => t.ImageUrl, s => s.MapFrom(x => x.Image == null ? "No image available" : "/images/trips/" + x.Image.Id + x.Image.Extentsion));
        }
    }
}

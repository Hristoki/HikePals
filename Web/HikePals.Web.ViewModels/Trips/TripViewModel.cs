namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public class TripViewModel : BaseTripViewModel, IMapFrom<Trip>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string LocationCityName { get; set; }

        public string UserId { get; set; }

        public double AverageRating { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Trip, TripViewModel>()
                .ForMember(t => t.ImageUrl, s =>
                    s.MapFrom(x =>
                        x.Image == null ? "No image available" : "/images/trips/" + x.Image.Id + x.Image.Extentsion))
                .ForMember(x => x.AverageRating, t => t.MapFrom(y => y.Rating.Count() == 0 ? 0 : y.Rating.Average(z => z.Value )));
        }
    }
}
namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

   public class BaseTripViewModel: IMapFrom<Trip>
    {
        public int Id { get; set; }

        public string TripImageUrl { get; set; }

        public string Name { get; set; }

        public int LocationCategoryId { get; set; }

        public string LocationCategoryName { get; set; }


        // TO DO: Remove Later
       //public void CreateMappings(IProfileExpression configuration)
       //{
       //    configuration.CreateMap<Trip, BaseTripViewModel>()
       //        .ForMember(x => x.Name, opt =>
       //            opt.MapFrom(x =>
       //                x.TripName.Take(100)));
       //}
    }
}

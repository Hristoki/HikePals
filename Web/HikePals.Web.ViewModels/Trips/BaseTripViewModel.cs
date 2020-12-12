namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public abstract class BaseTripViewModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string Title { get; set; }

        [Required]
        [MaxLength(300)]
        [MinLength(3)]
        public string LocationName { get; set; }

        public string ImageUrl { get; set; }

        public int LocationCategoryId { get; set; }

        public string LocationCategoryName { get; set; }

        public int Distance { get; set; }

        public TimeSpan Duration { get; set; }

        [Required]
        [MaxLength(10000, ErrorMessage = "Description should be between 50 and 500 symbols long")]
        [MinLength(25, ErrorMessage = "Description should be between 50 and 500 symbols long")]

        public string Description { get; set; }

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

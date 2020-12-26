namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.ComponentModel.DataAnnotations;


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

        [Required]
        public int LocationCategoryId { get; set; }

        [Required]
        public string LocationCategoryName { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Distance { get; set; }

        [Required]
        [Range(0, 1000)]
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

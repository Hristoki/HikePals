namespace HikePals.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public class SingleCategoryViewModel : IMapFrom<Category>, IHaveCustomMappings

    {
        public string Name { get; set; }

        public int TripsCount { get; set; }

        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, SingleCategoryViewModel>()
                .ForMember(x => x.TripsCount, s => s.MapFrom(x => x.Locations.Count));
        }
    }
}

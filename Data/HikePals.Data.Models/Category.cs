using HikePals.Data.Common.Models;
using System.Collections.Generic;

namespace HikePals.Data.Models
{
    public class Category: BaseDeletableModel<int>
    {
        public Category()
        {
            this.Locations = new HashSet<Location>();
        }

        public string Name { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
namespace HikePals.Data.Models
{
    using System.Collections.Generic;

    using HikePals.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Locations = new HashSet<Location>();
        }

        public string Name { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
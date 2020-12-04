// ReSharper disable VirtualMemberCallInConstructor
namespace HikePals.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using HikePals.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Ratings = new HashSet<Rating>();
            this.Messages = new HashSet<Message>();
            this.JoinedTrips = new HashSet<EventsUsers>();
            this.Events = new HashSet<Event>();
        }

        public string Name { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Trip> CreatedTrips { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<EventsUsers> JoinedTrips { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}

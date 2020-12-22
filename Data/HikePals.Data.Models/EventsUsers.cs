// ReSharper disable VirtualMemberCallInConstructor
using HikePals.Data.Common.Models;

namespace HikePals.Data.Models
{
    public class EventsUsers : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }
    }
}

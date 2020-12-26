// ReSharper disable VirtualMemberCallInConstructor
namespace HikePals.Data.Models
{
    using HikePals.Data.Common.Models;

    public class EventsUsers : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public bool PendingJoinRequest { get; set; }
    }
}

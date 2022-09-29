using System;
using System.Collections.Generic;

namespace CSBooking.Models
{
    public partial class TicketCategory
    {
        public TicketCategory()
        {
            EventsDetails = new HashSet<EventsDetail>();
        }

        public int TicketCategoryId { get; set; }
        public string? TicketCategoryName { get; set; }
        public int? TicketsAvailable { get; set; }

        public virtual ICollection<EventsDetail> EventsDetails { get; set; }
    }
}

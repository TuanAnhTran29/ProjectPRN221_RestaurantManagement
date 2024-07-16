using System;
using System.Collections.Generic;

namespace ProjectPRN_RestaurantManagement.Models
{
    public partial class Table
    {
        public Table()
        {
            SeatBookings = new HashSet<SeatBooking>();
        }

        public int TableId { get; set; }
        public int? RestaurantId { get; set; }
        public string TableNumber { get; set; } = null!;
        public int Capacity { get; set; }

        public virtual Restaurant? Restaurant { get; set; }
        public virtual ICollection<SeatBooking> SeatBookings { get; set; }
    }
}

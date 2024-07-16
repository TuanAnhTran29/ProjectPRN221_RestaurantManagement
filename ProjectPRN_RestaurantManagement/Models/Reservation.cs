using System;
using System.Collections.Generic;

namespace ProjectPRN_RestaurantManagement.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            SeatBookings = new HashSet<SeatBooking>();
        }

        public int ReservationId { get; set; }
        public int? CustomerId { get; set; }
        public int? RestaurantId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }

        public virtual User? Customer { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        public virtual ICollection<SeatBooking> SeatBookings { get; set; }
    }
}

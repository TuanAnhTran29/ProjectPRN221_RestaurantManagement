using System;
using System.Collections.Generic;

namespace ProjectPRN_RestaurantManagement.Models
{
    public partial class SeatBooking
    {
        public int SeatBookingId { get; set; }
        public int? ReservationId { get; set; }
        public int? TableId { get; set; }
        public int SeatCount { get; set; }
        public virtual Table? Table { get; set; }
        public virtual Reservation? Reservation { get; set; }
    }
}

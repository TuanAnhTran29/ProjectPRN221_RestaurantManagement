using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;
using ProjectPRN_RestaurantManagement.Models.SessionManagement;

namespace ProjectPRN_RestaurantManagement.Pages.Manager
{
    public class SeatBookingModel : PageModel
    {
        private readonly RestaurantManagementContext context;

        public SeatBookingModel(RestaurantManagementContext _context)
        {
            context= _context;
        }
        public void OnGet(int? reservationId, int? tableId)
        {
            // Retrieve the user object from session
            var LoggedInUser = HttpContext.Session.GetObjectFromJson<String>("User");
            User? user = context.Users.FirstOrDefault(u => u.Email.Equals(LoggedInUser));
            ViewData["user"] = user;

            var seatBookings = context.SeatBookings
                .Include(s => s.Table)
                .Include(s => s.Reservation)
                .Include(s => s.Reservation.Customer)
                .Include(s => s.Reservation.Restaurant)
                .Where(s => s.ApproveDate == null && s.DeleteDate == null)
                .ToList();
            ViewData["seat_booking"]= seatBookings;

            if (reservationId != null)
            {
                var seat = context.SeatBookings.FirstOrDefault(s => s.ReservationId == reservationId);

                seat.ApproveDate = DateTime.Now;

                context.SeatBookings.Update(seat);

                context.SaveChanges();

                ViewData["messasge"] = "Approved Seat successfully!";

            }else if (tableId != null)
            {
                var seat = context.SeatBookings.FirstOrDefault(s => s.TableId == tableId);

                seat.DeleteDate = DateTime.Now;

                context.SeatBookings.Update(seat);

                context.SaveChanges();

                ViewData["messasge"] = "Rejected Seat successfully!";
            }
        }
    }
}

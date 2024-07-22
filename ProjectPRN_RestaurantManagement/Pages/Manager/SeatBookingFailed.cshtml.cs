using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN_RestaurantManagement.Models.SessionManagement;
using ProjectPRN_RestaurantManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectPRN_RestaurantManagement.Pages.Manager
{
    public class SeatBookingFailedModel : PageModel
    {
        private readonly RestaurantManagementContext context;

        public SeatBookingFailedModel(RestaurantManagementContext _context)
        {
            context = _context;
        }
        public void OnGet()
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
                .Where(s => s.DeleteDate != null)
                .ToList();
            ViewData["seat_booking"] = seatBookings;
        }
    }
}

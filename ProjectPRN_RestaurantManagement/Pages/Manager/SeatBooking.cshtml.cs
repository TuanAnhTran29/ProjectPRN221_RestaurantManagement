using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Manager
{
    public class SeatBookingModel : PageModel
    {
        private readonly RestaurantManagementContext context;

        public SeatBookingModel(RestaurantManagementContext _context)
        {
            context= _context;
        }
        public void OnGet()
        {
        }
    }
}

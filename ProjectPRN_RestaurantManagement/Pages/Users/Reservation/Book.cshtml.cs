using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Users.Reservation
{
    public class BookModel : PageModel
    {
        private readonly RestaurantManagementContext _context;

        public BookModel(RestaurantManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Reservation Reservation { get; set; }

        public Restaurant Restaurant { get; set; }
        public IList<Table> Tables { get; set; }

        public async Task<IActionResult> OnGetAsync(int restaurantId)
        {
            Restaurant = await _context.Restaurants
                .Include(r => r.Tables)
                .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);

            if (Restaurant == null)
            {
                return NotFound();
            }

            Tables = Restaurant.Tables.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}

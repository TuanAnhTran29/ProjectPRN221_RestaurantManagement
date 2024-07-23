using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Admin.Menus
{
    public class CreateModel : PageModel
    {
        private readonly RestaurantManagementContext _context = new();
        [BindProperty]
        public Menu MenuCreate { get; set; } = default!;
        [BindProperty]
        public IList<Restaurant> Restaurants { get; set; } = default!;

        public CreateModel(RestaurantManagementContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Restaurants = await _context.Restaurants.ToListAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.Menus.AddAsync(MenuCreate);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Admin/Menus/Index");
                }
                return Page();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "An error occurred. The data might already exist.");
                return Page();
            }
        }
    }
}

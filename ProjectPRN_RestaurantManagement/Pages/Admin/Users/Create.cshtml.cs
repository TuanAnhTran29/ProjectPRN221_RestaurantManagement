using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Admin.Users
{
    public class CreateModel : PageModel
    {
        private readonly RestaurantManagementContext _context = new();
        [BindProperty]
        public User UserCreate { get; set; } = default!;
        [BindProperty]
        public IList<Role> Roles { get; set; } = default!;

        public CreateModel(RestaurantManagementContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Roles = await _context.Roles.ToListAsync();

        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.Users.AddAsync(UserCreate);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Admin/Users/Index");
                }
                return Page();
            } catch (DbUpdateException ex) // Replace with specific exception type if known
            {
                ModelState.AddModelError("", "An error occurred. The data might already exist.");
                // Optionally set a flag to prevent form submission
                return Page();
            }
        }
    }
}

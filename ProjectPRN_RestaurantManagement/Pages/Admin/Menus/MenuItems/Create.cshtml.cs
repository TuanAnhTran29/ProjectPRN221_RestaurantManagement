using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Admin.Menus.MenuItems
{
    public class CreateModel : PageModel
    {
        private readonly RestaurantManagementContext _context = new();
        [BindProperty]
        public MenuItem MenuItemCreate { get; set; } = default!;
        [BindProperty]
        public int MenuId { get; set; } = default!;

        public CreateModel(RestaurantManagementContext context)
        {
            _context = context;
        }

        public void OnGet(int menuId)
        {
            MenuId = menuId;
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.MenuItems.AddAsync(MenuItemCreate);
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Admin.Menus.MenuItems
{
    public class EditModel : PageModel
    {
        private readonly RestaurantManagementContext _context = new();
        [BindProperty]
        public MenuItem MenuItemEdit { get; set; } = default!;

        public EditModel(RestaurantManagementContext context)
        {
            _context = context;
        }

        public void OnGet(int menuItemId)
        {
            MenuItemEdit = _context.MenuItems.FirstOrDefault(u => u.MenuItemId == menuItemId);
        }

        public async Task<IActionResult> OnPost()
        {
            _context.MenuItems.Update(MenuItemEdit);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/Menus/Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Admin.Menus.MenuItems
{
    public class IndexModel : PageModel
    {
        private readonly RestaurantManagementContext _context = new();
        public IList<MenuItem> MenuItems { get; set; } = default!;
        public int MenuId { get; set; }
        public async Task OnGetAsync(int menuId)
        {
            MenuItems = await _context.MenuItems.Where(mi => mi.MenuId == menuId).ToListAsync();
            MenuId = menuId;
        }

        public IActionResult OnGetDeleteMenuItem(int? menuItemId)
        {
            if (menuItemId != null)
            {
                var mi = _context.MenuItems.FirstOrDefault(mi => mi.MenuItemId == menuItemId);
                if (mi != null)
                {
                    _context.MenuItems.Remove(mi);
                    _context.SaveChanges();
                }
            }
            return RedirectToPage("/Admin/Menus/Index");
        }
    }
}

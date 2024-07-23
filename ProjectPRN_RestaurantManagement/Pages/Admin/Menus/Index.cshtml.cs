using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Admin.Menus
{
    public class IndexModel : PageModel
    {
        private readonly RestaurantManagementContext _context = new();

        public IList<Menu> Menus { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Menus = await _context.Menus.Include(u => u.Restaurant).ToListAsync();
        }

        public IActionResult OnGetDeleteMenu(int? menuId)
        {
            if (menuId != null)
            {
                var m = _context.Menus.FirstOrDefault(m => m.MenuId == menuId);
                if (m != null)
                {
                    _context.Menus.Remove(m);
                    _context.SaveChanges();
                }
            }
            return RedirectToPage("/Admin/Menus/Index");
        }
    }
}

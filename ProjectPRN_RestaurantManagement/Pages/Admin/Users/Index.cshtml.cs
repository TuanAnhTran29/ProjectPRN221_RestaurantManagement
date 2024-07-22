using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly RestaurantManagementContext _context = new();

        public IList<User> Users { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Users = await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public IActionResult OnGetDeleteUser(int? userId)
        {
            if (userId != null)
            {
                var u = _context.Users.FirstOrDefault(u => u.UserId == userId);
                if (u != null)
                {
                    _context.Users.Remove(u);
                    _context.SaveChanges();
                }
            }
            return RedirectToPage("/Admin/Users/Index");
        }
    }
}
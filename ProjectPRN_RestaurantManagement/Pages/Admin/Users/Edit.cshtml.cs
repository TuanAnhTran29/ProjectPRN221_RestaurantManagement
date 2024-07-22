using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Admin.Users
{
    public class EditModel : PageModel
    {
        private readonly RestaurantManagementContext _context = new();
        [BindProperty]
        public User UserEdit { get; set; } = default!;
        [BindProperty]
        public IList<Role> Roles { get; set; } = default!;

        public EditModel(RestaurantManagementContext context)
        {
            _context = context;
        }

        public void OnGet(int id)
        {
            UserEdit = _context.Users.FirstOrDefault(u => u.UserId == id);

            Roles = _context.Roles.ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            _context.Users.Update(UserEdit);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/Users/Index");
        }
    }
}

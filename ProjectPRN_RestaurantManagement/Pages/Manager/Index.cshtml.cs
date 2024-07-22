using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN_RestaurantManagement.Models;
using ProjectPRN_RestaurantManagement.Models.SessionManagement;

namespace ProjectPRN_RestaurantManagement.Pages.Manager
{
    public class IndexModel : PageModel
    {
        private readonly RestaurantManagementContext context;

        public IndexModel(RestaurantManagementContext _context)
        {
            this.context = _context;
        }
        public void OnGet()
        {
            // Retrieve the user object from session
            var LoggedInUser = HttpContext.Session.GetObjectFromJson<String>("User");
            User? user = context.Users.FirstOrDefault(u => u.Email.Equals(LoggedInUser));
            ViewData["user"] = user;
        }
    }
}

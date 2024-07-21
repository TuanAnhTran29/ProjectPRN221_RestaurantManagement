using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;
using ProjectPRN_RestaurantManagement.Models.SessionManagement;

namespace ProjectPRN_RestaurantManagement.Pages.Manager
{
    public class UpdateProfileModel : PageModel
    {
        private readonly RestaurantManagementContext context;

        public UpdateProfileModel(RestaurantManagementContext _context)
        {
            this.context = _context;
        }
        public void OnGet(string email)
        {
            var LoggedInUser = HttpContext.Session.GetObjectFromJson<String>("User");
            User? user = context.Users.FirstOrDefault(u => u.Email.Equals(LoggedInUser));
            ViewData["user"] = user;
        }
    }
}

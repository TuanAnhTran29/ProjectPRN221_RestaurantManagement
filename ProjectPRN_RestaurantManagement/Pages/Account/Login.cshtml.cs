using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;
using System.Security.Claims;

namespace ProjectPRN_RestaurantManagement.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly RestaurantManagementContext context;

        public LoginModel(RestaurantManagementContext restaurantManagementContext)
        {
            context = restaurantManagementContext;
        }
        public void OnGet()
        {
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == Input.Email.Trim() && u.PasswordHash == Input.Password.Trim());

            if (user == null)
            {
                String message = "Email or password is wrong! Try again";
                ViewData["message"] = message;
                return Page();
            }

            if (Input.IsRemember)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.RoleName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

                await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));
            }
            
            return RedirectToPage("/Manager/Index");
        }
    }
}

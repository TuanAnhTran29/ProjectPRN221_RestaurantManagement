using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN_RestaurantManagement.Models;
using System.Text.RegularExpressions;

namespace ProjectPRN_RestaurantManagement.Pages.Account
{
    public class ChangePasswordFormModel : PageModel
    {
        private readonly RestaurantManagementContext context;
        private static readonly String REGEX_PASSWORD = "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";

        public ChangePasswordFormModel(RestaurantManagementContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public string? newPassword { get; set; }

        [BindProperty]
        public string? confirmNewPassword { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (newPassword.Trim() == null || confirmNewPassword.Trim() == null)
            {
                return Page();
            }

            Regex regexPassword= new Regex(REGEX_PASSWORD);
            if (!regexPassword.IsMatch(newPassword.Trim()) || !regexPassword.IsMatch(confirmNewPassword.Trim()))
            {
                String message = "Password must be at least 8 characters long, 1 uppercase letter, 1 lowercase letter, 1 digit and 1 special character ";
                ViewData["message"] = message;
                return Page();
            }

            if (!newPassword.Trim().Equals(confirmNewPassword.Trim()))
            {
                String message = "Confirm password must be matched!";
                ViewData["message"] = message;
                return Page();
            }

            string userEmail = TempData["email"]?.ToString();
            var user = context.Users.FirstOrDefault(u => u.Email.Equals(userEmail));

            user.PasswordHash= newPassword.Trim();
            context.Users.Update(user);

            context.SaveChanges();

            TempData["SignupMessage"] = "You have changed password successfully!";

            return RedirectToPage("Login");
        }
    }
}

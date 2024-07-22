using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN_RestaurantManagement.Models;
using ProjectPRN_RestaurantManagement.Models.SessionManagement;
using System.Text.RegularExpressions;

namespace ProjectPRN_RestaurantManagement.Pages.Manager
{
    public class ChangePasswordModel : PageModel
    {
        private readonly RestaurantManagementContext context;

        private static readonly String REGEX_PASSWORD = "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";

        public ChangePasswordModel(RestaurantManagementContext _context)
        {
            this.context = _context;
        }

        [BindProperty]
        public ChangePasswordInputModel Input { get; set; }
        public void OnGet()
        {
            var LoggedInUser = HttpContext.Session.GetObjectFromJson<String>("User");
            User? user = context.Users.FirstOrDefault(u => u.Email.Equals(LoggedInUser));
            ViewData["user"] = user;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var LoggedInUser = HttpContext.Session.GetObjectFromJson<String>("User");
            User? user = context.Users.FirstOrDefault(u => u.Email.Equals(LoggedInUser));

            if (Input.currentPassword.Trim() == null
                || Input.newPassword.Trim() == null
                || Input.verifyNewPassword.Trim() == null)
            {
                String message = "Please fill out the form!";
                ViewData["message"] = message;
                OnGet();
                return Page();
            }

            Regex regexPassword = new Regex(REGEX_PASSWORD);
            if (!regexPassword.IsMatch(Input.newPassword.Trim()))
            {
                String message = "Password must be at least 8 characters long, 1 uppercase letter, 1 lowercase letter, 1 digit and 1 special character ";
                ViewData["message"] = message;
                OnGet();
                return Page();
            }

            if (!user.PasswordHash.Equals(Input.currentPassword))
            {
                String message = "Wrong password! Try again";
                ViewData["message"] = message;
                OnGet();
                return Page();
            }

            if (!Input.newPassword.Trim().Equals(Input.verifyNewPassword.Trim()))
            {
                String message = "Confirm password must be matched!";
                ViewData["message"] = message;
                return Page();
            }


            user.PasswordHash = Input.newPassword.Trim();
            user.UpdateAt = DateTime.Now;
            user.UpdateBy = user.UserId;

            context.Users.Update(user);

            context.SaveChanges();

            String successMessage = "Your new password has been updated!";
            ViewData["message"] = successMessage;
            OnGet();
            return Page();
        }
    }
}

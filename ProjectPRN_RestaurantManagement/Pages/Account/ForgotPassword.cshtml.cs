using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly RestaurantManagementContext context;
        private EmailSender emailSender= new EmailSender();

        public ForgotPasswordModel(RestaurantManagementContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public string? inputEmail { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if(inputEmail.Trim() == null)
            {
                ViewData["message"] = "Please enter your email to reset password";
                return Page();
            }
            var user= context.Users.FirstOrDefault(u => u.Email.Equals(inputEmail.Trim()));

            if(user == null)
            {
                ViewData["message"] = "User does not exist! Please enter an existed email";
                return Page();
            }

            int code = emailSender.generateAuthenticationCode();
            TempData["verification_code"] = code;
            TempData["email"] = inputEmail.Trim();

            await emailSender.sendEmailAsync(inputEmail.Trim(), "Verification Register", "Thanks for registering! This is your verification code: " + code + ". Enter this to complete register.");

            return RedirectToPage("VerifyForgotPassword");
        }
    }
}

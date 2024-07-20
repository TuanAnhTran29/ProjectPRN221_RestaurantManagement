using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectPRN_RestaurantManagement.Pages.Account
{
    public class VerifyForgotPasswordModel : PageModel
    {
        private EmailSender emailSender= new EmailSender();

        public void OnGet()
        {
        }

        [BindProperty]
        public string? inputVerifyCode {  get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            int code = int.Parse(TempData["verification_code"]?.ToString());
            if (!emailSender.isAuthenticated(code, int.Parse(inputVerifyCode)))
            {
                ViewData["message"] = "Invalid verification code! Enter again.";
                return Page();
            }

            TempData["email"] = TempData["email"]?.ToString();
            return RedirectToPage("ConfirmVerifyForChangePassword");
        }
    }
}

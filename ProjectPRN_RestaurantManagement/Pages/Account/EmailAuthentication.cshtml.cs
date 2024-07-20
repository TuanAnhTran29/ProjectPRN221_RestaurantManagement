using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Account
{
    public class EmailAuthenticationModel : PageModel
    {

        private readonly RestaurantManagementContext context;
        private readonly IEmailSender emailSender= new EmailSender();

        public EmailAuthenticationModel(RestaurantManagementContext _context)
        {
            this.context = _context;
        }
        public void OnGet()
        {
        }

        [BindProperty]
        public string inputVerifyCode { get; set; }

        public async Task<IActionResult> OnPost() {

            int code= int.Parse(TempData["verification_code"]?.ToString());
            if (!emailSender.isAuthenticated(code,int.Parse(inputVerifyCode.Trim())))
            {
                ViewData["message"] = "Invalid verification code! Enter again.";
                return Page();
            }

            var user = new User();
            user.Email = TempData["email"]?.ToString();
            user.RoleId = 3;
            user.FirstName = TempData["first_name"]?.ToString();
            user.LastName = TempData["last_name"]?.ToString();
            user.PasswordHash = TempData["password"]?.ToString();
            user.Phone = TempData["phone"]?.ToString();
            user.Address = TempData["address"]?.ToString();

            context.Users.Add(user);

            context.SaveChanges();

            String message = "Your account has been created successfully!";
            TempData["SignupMessage"] = message;

            return RedirectToPage("Login");
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ProjectPRN_RestaurantManagement.Pages.Account
{
    public class SignUpModel : PageModel
    {
        private readonly RestaurantManagementContext context;

        public SignUpModel(RestaurantManagementContext _context)
        {
            context = _context;
        }
        public void OnGet()
        {
        }

        [BindProperty]
        public SignUpInputModel Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(Input.Email == null 
                || Input.Password == null 
                || Input.ConfirmPassword == null 
                || Input.Email.Length == 0 
                || Input.Password.Length == 0 
                || Input.ConfirmPassword.Length == 0)
            {
                String message = "Please fill out the form!";
                ViewData["message"] = message;
                ModelState.AddModelError(string.Empty, "Invalid sign up attempt.");
            }

            String emailPattern = "^[\\w!#$%&'*+/=?`{|}~^-]+(?:\\.[\\w!#$%&'*+/=?`{|}~^-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailPattern);
            if (!regex.IsMatch(Input.Email))
            {
                String message = "Invalid email! Try again";
                ViewData["message"] = message;
                return Page();
            }
            if (!Input.Password.Equals(Input.ConfirmPassword))
            {
                String message = "Confirm password must be matched with password!";
                ViewData["message"] = message;
                return Page();
            }

            var emailExistUser = await context.Users.FirstOrDefaultAsync(m => m.Email.Equals(Input.Email));
            var phoneExistUser = await context.Users.FirstOrDefaultAsync(m => m.Phone.Equals(Input.PhoneNumber));
            if(emailExistUser != null)
            {
                String message = "Email existed! Try again";
                ViewData["message"] = message;
                return Page();
            }else if (phoneExistUser != null)
            {
                String message = "Phone existed! Try again";
                ViewData["message"] = message;
                return Page();
            }
            else
            {
                var user = new User();
                user.Email = Input.Email;
                user.RoleId = 3;
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.PasswordHash = Input.Password;
                user.Phone = Input.PhoneNumber;
                user.Address= Input.Address;

                context.Users.Add(user);

                context.SaveChanges();

                String message = "Registered successfully! Please login";
                ViewData["message"] = message;
            }
            
            return RedirectToPage("/Account/Login");
        }
    }
}

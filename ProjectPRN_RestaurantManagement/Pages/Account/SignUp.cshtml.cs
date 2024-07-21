using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectPRN_RestaurantManagement.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ProjectPRN_RestaurantManagement.Pages.Account
{
    public class SignUpModel : PageModel
    {
        private readonly RestaurantManagementContext context;
        private IEmailSender emailSender= new EmailSender();

        private static readonly String REGEX_FIRST_LAST_NAME= "^[A-Z][a-z]+(?:[-' ][A-Z][a-z]+)*$";
        private static readonly String REGEX_EMAIL= "^[\\w!#$%&'*+/=?`{|}~^-]+(?:\\.[\\w!#$%&'*+/=?`{|}~^-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,}$";
        private static readonly String REGEX_PASSWORD = "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";
        private static readonly String REGEX_PHONE = "^(?:(?:\\+?84|0)(?:\\d{1,2})(?:\\s|-|\\.)\\d{3}(?:\\s|-|\\.)\\d{3})|(?:\\d{10})$";
        private static readonly String REGEX_ADDRESS = "^([^0-9\\.]+)(?:\\s+|\\.)?([0-9\\.]+)?(?:\\s*(?:[Pp.]\\.?\\s*[Hh]?\\.?\\s*)?([^\\.]+))?(?:\\s*(?:[Qq.]\\.?\\s*[Uu]\\.?\\s*)?([^\\.]+))?(?:\\s*(?:[Dd.]\\.?\\s*[Tt]\\.?\\s*)?([^\\.]+))?(?:\\s*(?:[Pp]\\.?\\s*[Hh]\\.?\\s*)?([^\\.]+))?\\s*$";
        private static readonly String REGEX_USERNAME = "^[a-zA-Z0-9_-]{3,20}$";
        public SignUpModel(RestaurantManagementContext _context)
        {
            context = _context;
        }

        [BindProperty]
        public SignUpInputModel Input { get; set; }

        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPostAsync()
        {

            if (Input.Username.Trim() == null
                || Input.FirstName.Trim() == null
                || Input.LastName.Trim() == null
                || Input.PhoneNumber.Trim() == null
                || Input.Email.Trim() == null
                || Input.Password.Trim() == null
                || Input.ConfirmPassword.Trim() == null
                || Input.Address.Trim() == null)
            {
                String message = "Please fill out the form!";
                ViewData["message"] = message;Page();
                return Page();
            }
            Regex regexUsername = new Regex(REGEX_USERNAME);
            if (!regexUsername.IsMatch(Input.Username.Trim()))
            {
                String message = "Username must have between 3 - 20 characters long";
                ViewData["message"] = message;
                return Page();
            }


            Regex regexFirstLastName = new Regex(REGEX_FIRST_LAST_NAME);
            if (!regexFirstLastName.IsMatch(Input.FirstName.Trim()) || !regexFirstLastName.IsMatch(Input.LastName.Trim()))
            {
                String message = "First name or last name is invalid";
                ViewData["message"] = message;
                return Page();
            }
            Regex regexEmail = new Regex(REGEX_EMAIL);
            if (!regexEmail.IsMatch(Input.Email.Trim()))
            {
                String message = "Invalid email! Try again";
                ViewData["message"] = message;
                return Page();
            }
            Regex regexPhone= new Regex(REGEX_PHONE);
            if (!regexPhone.IsMatch(Input.PhoneNumber.Trim()))
            {
                String message = "Invalid phone number! Try again";
                ViewData["message"] = message;
                return Page();
            }

            Regex regexPassword = new Regex(REGEX_PASSWORD);
            if (!regexPassword.IsMatch(Input.Password.Trim()))
            {
                String message = "Password must be at least 8 characters long, 1 uppercase letter, 1 lowercase letter, 1 digit and 1 special character ";
                ViewData["message"] = message;
                return Page();
            }

            if (!Input.Password.Equals(Input.ConfirmPassword.Trim()))
            {
                String message = "Confirm password must be matched!";
                ViewData["message"] = message;
                return Page();
            }

            Regex regexAddress = new Regex(REGEX_ADDRESS);
            if (!regexAddress.IsMatch(Input.Address.Trim()))
            {
                String message = "Invalid address! Try again";
                ViewData["message"] = message;
                return Page();
            }

            if (!Input.IsAgree)
            {
                String message = "You have to accept the Term of Use";
                ViewData["message"] = message;
                return Page();
            }

            var emailExistUser = await context.Users.FirstOrDefaultAsync(m => m.Email.Equals(Input.Email.Trim()));
            var phoneExistUser = await context.Users.FirstOrDefaultAsync(m => m.Phone.Equals(Input.PhoneNumber.Trim()));
            var usernameExistUser= await context.Users.FirstOrDefaultAsync(m => m.Username.Equals(Input.Username.Trim()));

            if (usernameExistUser != null)
            {
                String message = "Username existed! Try again";
                ViewData["message"] = message;
                return Page();
            }
            else if (emailExistUser != null)
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
                TempData["username"] = Input.Username.Trim();
                TempData["first_name"] = Input.FirstName.Trim();
                TempData["last_name"] = Input.LastName.Trim();
                TempData["email"] = Input.Email.Trim();
                TempData["address"] = Input.Address.Trim();
                TempData["password"] = Input.Password.Trim();
                TempData["phone"] = Input.PhoneNumber.Trim();

                int code = emailSender.generateAuthenticationCode();
                TempData["verification_code"] = code;

                await emailSender.sendEmailAsync(Input.Email, "Verification Register", "Thanks for registering! This is your verification code: " + code + ". Enter this to complete register.");
            }
            
            return RedirectToPage("EmailAuthentication");
        }
    }
}

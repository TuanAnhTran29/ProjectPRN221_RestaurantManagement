using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;
using ProjectPRN_RestaurantManagement.Models.SessionManagement;
using ProjectPRN_RestaurantManagement.Pages.Account;
using System.Text.RegularExpressions;

namespace ProjectPRN_RestaurantManagement.Pages.Manager
{
    public class UserProfileModel : PageModel
    {
        private readonly RestaurantManagementContext context;

        private static readonly String REGEX_FIRST_LAST_NAME = "^[A-Z][a-z]+(?:[-' ][A-Z][a-z]+)*$";
        private static readonly String REGEX_PHONE = "^(?:(?:\\+?84|0)(?:\\d{1,2})(?:\\s|-|\\.)\\d{3}(?:\\s|-|\\.)\\d{3})|(?:\\d{10})$";
        private static readonly String REGEX_ADDRESS = "^([^0-9\\.]+)(?:\\s+|\\.)?([0-9\\.]+)?(?:\\s*(?:[Pp.]\\.?\\s*[Hh]?\\.?\\s*)?([^\\.]+))?(?:\\s*(?:[Qq.]\\.?\\s*[Uu]\\.?\\s*)?([^\\.]+))?(?:\\s*(?:[Dd.]\\.?\\s*[Tt]\\.?\\s*)?([^\\.]+))?(?:\\s*(?:[Pp]\\.?\\s*[Hh]\\.?\\s*)?([^\\.]+))?\\s*$";
        private static readonly String REGEX_USERNAME = "^[a-zA-Z0-9_-]{3,20}$";

        public UserProfileModel(RestaurantManagementContext _context)
        {
            this.context = _context;
        }

        [BindProperty]
        public UpdateProfileInputModel Input { get; set; }

        public void OnGet()
        {
            var LoggedInUser = HttpContext.Session.GetObjectFromJson<String>("User");
            User? user = context.Users.FirstOrDefault(u => u.Email.Equals(LoggedInUser));
            ViewData["user"] = user;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var loginUser = HttpContext.Session.GetObjectFromJson<String>("User");
            User? currentUser = context.Users.FirstOrDefault(u => u.Email.Equals(loginUser));

            if (Input.Username == null
                || Input.FirstName == null
                || Input.LastName== null
                || Input.PhoneNumber== null
                || Input.Address == null
                || Input.Gender == null)
            {
                String message = "Please fill out the form!";
                ViewData["message"] = message;
                OnGet();
                return Page();
            }
            Regex regexUsername = new Regex(REGEX_USERNAME);
            if (!regexUsername.IsMatch(Input.Username.Trim()))
            {
                String message = "Username must have between 3 - 20 characters long";
                ViewData["message"] = message;
                OnGet();
                return Page();
            }


            Regex regexFirstLastName = new Regex(REGEX_FIRST_LAST_NAME);
            if (!regexFirstLastName.IsMatch(Input.FirstName.Trim()) || !regexFirstLastName.IsMatch(Input.LastName.Trim()))
            {
                String message = "First name or last name is invalid";
                ViewData["message"] = message;
                OnGet();
                return Page();
            }
            Regex regexPhone = new Regex(REGEX_PHONE);
            if (!regexPhone.IsMatch(Input.PhoneNumber.Trim()))
            {
                String message = "Invalid phone number! Try again";
                ViewData["message"] = message;
                OnGet();
                return Page();
            }

            Regex regexAddress = new Regex(REGEX_ADDRESS);
            if (!regexAddress.IsMatch(Input.Address.Trim()))
            {
                String message = "Invalid address! Try again";
                ViewData["message"] = message;
                OnGet();
                return Page();
            }

            //if (!Input.IsAgree)
            //{
            //    String message = "You have to accept the Term of Use";
            //    ViewData["message"] = message;
            //    return Page();
            //}

            //var emailExistUser = await context.Users.FirstOrDefaultAsync(m => m.Email.Equals(Input.Email.Trim()));
            var phoneExistUser = await context.Users.FirstOrDefaultAsync(m => m.Phone.Equals(Input.PhoneNumber.Trim()) && !m.Phone.Equals(currentUser.Phone) && m.Email.Equals(HttpContext.Session.GetObjectFromJson<String>("User")));
            var usernameExistUser = await context.Users.FirstOrDefaultAsync(m => m.Username.Equals(Input.Username.Trim()) && !m.Username.Equals(currentUser.Username) && m.Email.Equals(HttpContext.Session.GetObjectFromJson<String>("User")));

            if (usernameExistUser != null)
            {
                String message = "Username existed! Try again";
                ViewData["message"] = message;
                OnGet();
                return Page();
            }
            //else if (emailExistUser != null)
            //{
            //    String message = "Email existed! Try again";
            //    ViewData["message"] = message;
            //    return Page();
            //}
            else if (phoneExistUser != null)
            {
                String message = "Phone existed! Try again";
                ViewData["message"] = message;
                OnGet();
                return Page();
            }
            else
            {
                var LoggedInUser = HttpContext.Session.GetObjectFromJson<String>("User");
                User? user = context.Users.FirstOrDefault(u => u.Email.Equals(LoggedInUser));

                user.FirstName = Input.FirstName.Trim();
                user.LastName= Input.LastName.Trim();
                user.Username = Input.Username.Trim();
                user.Gender= Input.Gender.Value;
                user.Dob = DateTime.Parse(Input.Dob);
                user.Address = Input.Address.Trim();
                user.UpdateAt = DateTime.Now;
                user.UpdateBy = user.UserId;

                context.Users.Update(user);

                context.SaveChanges();
            }
            ViewData["message"] = "Your profile have been updated successfully!";
            OnGet();
            
            return Page();
        }
    }
}

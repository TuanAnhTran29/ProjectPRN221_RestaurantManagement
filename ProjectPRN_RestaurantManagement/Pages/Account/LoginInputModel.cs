using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProjectPRN_RestaurantManagement.Pages.Account
{
    public class LoginInputModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [BindProperty]
        public bool IsRemember { get; set; } = true;
    }
}

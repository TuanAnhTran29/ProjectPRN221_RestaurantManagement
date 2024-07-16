using System.ComponentModel.DataAnnotations;

namespace ProjectPRN_RestaurantManagement.Pages.Account
{
    public class LoginInputModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

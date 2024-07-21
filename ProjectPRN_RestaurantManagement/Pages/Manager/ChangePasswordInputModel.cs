using System.ComponentModel.DataAnnotations;

namespace ProjectPRN_RestaurantManagement.Pages.Manager
{
    public class ChangePasswordInputModel
    {
        [Required]
        public string? currentPassword { get; set; }

        [Required]
        public string? newPassword { get; set; }

        [Required]
        public string? verifyNewPassword { get; set; }
    }
}

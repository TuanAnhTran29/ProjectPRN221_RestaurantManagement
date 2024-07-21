using System.ComponentModel.DataAnnotations;

namespace ProjectPRN_RestaurantManagement.Pages.Manager
{
    public class UpdateProfileInputModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public bool? Gender { get; set; }

        [Required]
        public string? Dob { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
namespace KarnelTravels.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string? Password { get; set; }

        [StringLength(50)]
        public string Role { get; set; } = "Admin";

        [Display(Name = "Created At")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}

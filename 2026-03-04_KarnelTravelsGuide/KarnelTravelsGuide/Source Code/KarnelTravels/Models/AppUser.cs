using Microsoft.AspNetCore.Identity;
namespace KarnelTravels.Models
{
    public class AppUser : IdentityUser
    {
        public string? Contact { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;
namespace KarnelTravels.Models
{
    public class Admin
    {
      
        public int AdminId { get; set; }

      
        public string? FullName { get; set; }


        public string? Email { get; set; }

  
        public string? Username { get; set; }

        public string? Password { get; set; }

        public string Role { get; set; } = "Admin";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}

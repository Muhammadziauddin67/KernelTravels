using System.ComponentModel.DataAnnotations.Schema;
namespace KarnelTravels.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}

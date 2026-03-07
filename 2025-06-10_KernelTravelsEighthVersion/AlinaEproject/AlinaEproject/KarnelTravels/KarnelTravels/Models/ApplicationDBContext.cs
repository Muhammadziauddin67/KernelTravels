using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KarnelTravels.Models;
namespace KarnelTravels.Models
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Tourism> Tourisms { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

    }
}

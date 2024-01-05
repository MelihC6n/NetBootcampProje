using Microsoft.EntityFrameworkCore;

namespace NetBootcampProje.Models
{
    public class ProjeDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-13V6IU8;Database=TechCareer;Trusted_Connection=True;");
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

    }
}

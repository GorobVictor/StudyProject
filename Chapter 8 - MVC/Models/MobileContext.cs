using Microsoft.EntityFrameworkCore;

namespace Chapter_8___MVC.Models
{
    public class MobileContext:DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }

        public MobileContext(DbContextOptions<MobileContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

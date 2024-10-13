using lb1.Models;
using Microsoft.EntityFrameworkCore;

namespace lb1
{
    public class ClassifiedsContext : DbContext
    {
        public ClassifiedsContext(DbContextOptions<ClassifiedsContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>()
                .Property(a => a.Price)
                .HasColumnType("decimal(18, 2)");

            
        }
    }
}

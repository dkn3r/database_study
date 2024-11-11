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

            modelBuilder.Entity<Ad>()
                .HasMany(a => a.Messages)
                .WithOne(m => m.Ad)
                .HasForeignKey(m => m.AdID)
                .OnDelete(DeleteBehavior.Cascade); // Налаштування каскадного видалення

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Ads)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryID)
                .OnDelete(DeleteBehavior.Cascade); // Налаштування каскадного видалення для категорій
        }

    }
}

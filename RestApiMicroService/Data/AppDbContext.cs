using DBModels.Models;
using Microsoft.EntityFrameworkCore;

namespace RestApiMicroService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<Exercises> Exercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja relacji jeden do wielu 
            modelBuilder.Entity<User>()
                .HasMany(t => t.Currencies);

            modelBuilder.Entity<Currency>()
              .HasMany(t => t.Users);

            modelBuilder.Entity<User>().HasData(
             new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Password = "password123" },
             new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Password = "password456" }
            );

            modelBuilder.Entity<Currency>().HasData(
              new Currency { Id = 1, Name = "Mike Johnson", Symbol = "$", Price= 1.2},
              new Currency { Id = 2, Name = "Emily Davis", Symbol = "$", Price = 0.1 }

          );

        }

    }
}

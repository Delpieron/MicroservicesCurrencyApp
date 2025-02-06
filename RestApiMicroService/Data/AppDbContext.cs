using Microsoft.EntityFrameworkCore;
using RestApiMicroService.Models;

namespace RestApiMicroService.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CryptoDatas)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "JohnDoe",
                    Email = "john.doe@example.com",
                    PasswordHash = "dummyHash1",
                    PasswordSalt = "dummySalt1"
                },
                new User
                {
                    Id = 2,
                    Username = "JaneSmith",
                    Email = "jane.smith@example.com",
                    PasswordHash = "dummyHash2",
                    PasswordSalt = "dummySalt2"
                }
            );

            // Seed data dla danych kryptowalutowych
            modelBuilder.Entity<Currency>().HasData(
                new Currency
                {
                    Id = 1,
                    Symbol = "BTC",
                    Price = 1.2m,
                    Timestamp = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc),
                    UserId = 1,
                    
                },
                new Currency
                {
                    Id = 2,
                    Symbol = "ETH",
                    Price = 0.1m,
                    Timestamp = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc),
                    UserId = 2
                }
            );

        }

    }
}

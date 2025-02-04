using DBModels.Models;
using Microsoft.EntityFrameworkCore;
namespace DBModels
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { } 
         
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Currency>()
                .HasMany(t => t.Users)
                .WithOne(p => p.Currency)
                .HasForeignKey(p => p.CurrencyId);
        }
    }
}

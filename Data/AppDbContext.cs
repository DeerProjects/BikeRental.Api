using Microsoft.EntityFrameworkCore;
using  Models; // or your models namespace

namespace Data; // <-- make sure this matches everywhere

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Bicycle> Bicycles => Set<Bicycle>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Rental> Rentals => Set<Rental>();

    protected override void OnModelCreating(ModelBuilder m)
    {
        m.Entity<Rental>()
         .HasOne(r => r.Bicycle).WithMany().HasForeignKey(r => r.BicycleId)
         .OnDelete(DeleteBehavior.Restrict);

        m.Entity<Rental>()
         .HasOne(r => r.Customer).WithMany().HasForeignKey(r => r.CustomerId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}

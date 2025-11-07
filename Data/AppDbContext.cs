using Microsoft.EntityFrameworkCore;
using  Models; // or your models namespace

namespace Data; // <-- make sure this matches everywhere

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Bicycle> Bicycles => Set<Bicycle>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Rental> Rentals => Set<Rental>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // Bicycle
        b.Entity<Bicycle>(e =>
        {
            e.Property(x => x.Model)
                .HasMaxLength(200)
                .IsRequired();

            e.Property(x => x.SerialNumber)
                .HasMaxLength(100)
                .IsRequired();

            e.HasIndex(x => x.SerialNumber).IsUnique();

            // Precision hint; SQLite stores as REAL but EF will round in memory.
            e.Property(x => x.HourlyRate).HasPrecision(10, 2);

            e.Property(x => x.IsAvailable).HasDefaultValue(true);

            e.Property(x => x.CreatedAt)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v.DateTime, DateTimeKind.Utc))
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Customer
        b.Entity<Customer>(e =>
        {
            e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            e.Property(x => x.LastName).HasMaxLength(100).IsRequired();

            e.Property(x => x.Email).HasMaxLength(256).IsRequired();
            e.HasIndex(x => x.Email).IsUnique();

            e.Property(x => x.Phone).HasMaxLength(32);

            e.Property(x => x.CreatedAt)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v.DateTime, DateTimeKind.Utc))
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Rental
        b.Entity<Rental>(e =>
        {
            e.HasOne(r => r.Bicycle)
             .WithMany(bc => bc.Rentals)
             .HasForeignKey(r => r.BicycleId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(r => r.Customer)
             .WithMany(c => c.Rentals)
             .HasForeignKey(r => r.CustomerId)
             .OnDelete(DeleteBehavior.Restrict);

            e.Property(x => x.StartAt).IsRequired();
            e.Property(x => x.EndAt);

            e.Property(x => x.TotalPrice).HasPrecision(10, 2);

            // Enum storage as int (default). If you prefer string, uncomment next line.
            // e.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            e.Property(x => x.Status).HasDefaultValue(RentalStatus.Active);

            // Helpful index for active lookups per customer/bicycle
            e.HasIndex(x => new { x.CustomerId, x.Status });
            e.HasIndex(x => new { x.BicycleId, x.Status });
        });
    }
}

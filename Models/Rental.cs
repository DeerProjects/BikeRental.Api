using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public enum RentalStatus
    {
        Active = 1,
        Completed = 2,
        Canceled = 3
    }
    public class Rental
    {
        public int Id { get; set; }

        public Bicycle Bicycle { get; set; } = null!;
        public int BicycleId { get; set; }

        public Customer Customer { get; set; } = null!;
        public int CustomerId { get; set; }


        public DateTimeOffset StartAt { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? EndAt { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? TotalPrice { get; set; }
        public RentalStatus Status { get; set; } = RentalStatus.Active;
    }
}
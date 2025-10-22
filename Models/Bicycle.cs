using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Bicycle
    {
       public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Model { get; set; } = null!;

        [Range(0, 1000)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal HourlyRate { get; set; } = 5.00m;

        public bool IsAvailable { get; set; } = true;
        
        [MaxLength(50)]
        public string? SerialNumber { get; set; }
        
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
  }
}
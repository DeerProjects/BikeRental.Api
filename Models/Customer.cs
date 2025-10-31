namespace Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    }
}
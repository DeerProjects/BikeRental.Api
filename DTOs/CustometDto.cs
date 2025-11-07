namespace BikeRental.Api
{
   public record CustomerReadDto(
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string? Phone,
        DateTimeOffset CreatedAt
        );

    public record CustomerCreateDto(
        string FirstName,
        string LastName,
        string Email,
        string? Phone
        );

    public record CustomerUpdateDto(
        string FirstName,
        string LastName,
        string Email,
        string? Phone
        );
}
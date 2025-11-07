using Models;

namespace BikeRental.Api;

//start time
public record RentalReadDto(
    int Id,
    int BicycleId,
    int CustomerId,
    DateTimeOffset StartAt,
    DateTimeOffset? EndAt,
    decimal? TotalPrice,
    RentalStatus Status
    );

// end time
public record RentalStartDto(
    int BicycleId,
    int CustomerId,
    DateTimeOffset StartAt // optional, server also assign default probably
    );

// cancel rental
public record RentalEndDto(
    DateTimeOffset? EndAt
    );

public record RentalCancelDto(
    string? Reason //make this optional and keep flexible because fo dont persist to customer bla bla
    );


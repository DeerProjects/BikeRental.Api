namespace BikeRental.Api;

public record BicycleReadDto(
    int Id,
    string Model,
    decimal HourlyRate,
    bool IsAvailable,
    string SerialNumber,
    DateTime CreatedAt
    );

public record BicycleCreateDto(
    string Model,
    decimal HourlyRate,
    string SerialNumber
    );

public record BicycleUpdateDto(
    string Model,
    decimal HourlyRate,
    bool IsAvailable
    );
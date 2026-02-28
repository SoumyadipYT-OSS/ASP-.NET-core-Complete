namespace AutoWorks.Api.DTOs;

public record ManufacturerCreateDto(
    string CompanyName,
    string? Country
);

public record ManufacturerReadDto(
    int ManufacturerId,
    string CompanyName,
    string? Country,
    DateTime CreatedAt
);

public record ManufacturerUpdateDto(
    string? CompanyName,
    string? Country
);
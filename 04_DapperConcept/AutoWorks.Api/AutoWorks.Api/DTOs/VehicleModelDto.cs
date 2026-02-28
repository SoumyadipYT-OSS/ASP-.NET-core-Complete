namespace AutoWorks.Api.DTOs;

public record VehicleModelCreateDto(
    int ManufacturerId,
    string ModelName,
    string? Segment
);

public record VehicleModelReadDto(
    int ModelId,
    int ManufacturerId,
    string ModelName,
    string? Segment,
    DateTime CreatedAt
);

public record VehicleModelUpdateDto(
    string? ModelName,
    string? Segment
);
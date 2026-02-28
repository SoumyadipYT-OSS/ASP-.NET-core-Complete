
namespace AutoWorks.Api.DTOs;


public record VehicleCreateDto
(
    int ModelId, 
    string Vin, 
    string? Color, 
    int MfgYear, 
    decimal Price, 
    string Status
);

public record VehicleReadDto
(
    int VehicleId,
    int ModelId,
    string Vin,
    string? Color,
    int MfgYear,
    decimal Price,
    string Status,
    DateTime CreatedAt
);

public record VehicleUpdateDto
(
    string? Color, 
    decimal? Price, 
    string? Status 
);

using AutoWorks.Api.DTOs;
using System.Data;
using Dapper;

namespace AutoWorks.Api.Repositories;

public class VehicleModelRepository {
    private readonly IDbConnection _db;
    public VehicleModelRepository(IDbConnection db) => _db = db;

    public async Task<IEnumerable<VehicleModelReadDto>> GetAllAsync(int page = 1, int pageSize = 20) {
        var sql = """
            SELECT ModelId, ManufacturerId, ModelName, Segment, CreatedAt
            FROM VehicleModels
            ORDER BY ModelId DESC
            OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;
        """;

        return await _db.QueryAsync<VehicleModelReadDto>(sql, new { offset = (page - 1) * pageSize, pageSize });
    }

    public async Task<VehicleModelReadDto?> GetByIdAsync(int id) {
        var sql = """
            SELECT ModelId, ManufacturerId, ModelName, Segment, CreatedAt
            FROM VehicleModels
            WHERE ModelId = @id;
        """;

        return await _db.QueryFirstOrDefaultAsync<VehicleModelReadDto>(sql, new { id });
    }

    public async Task<int> CreateAsync(VehicleModelCreateDto dto) {
        var sql = """
            INSERT INTO VehicleModels(ManufacturerId, ModelName, Segment)
            OUTPUT INSERTED.ModelId
            VALUES(@ManufacturerId, @ModelName, @Segment);
        """;

        return await _db.ExecuteScalarAsync<int>(sql, dto);
    }

    public async Task<bool> UpdateAsync(int id, VehicleModelUpdateDto dto) {
        var sql = """
            UPDATE VehicleModels
            SET ModelName = COALESCE(@ModelName, ModelName),
                Segment = COALESCE(@Segment, Segment)
            WHERE ModelId = @id;
        """;

        var rows = await _db.ExecuteAsync(sql, new { id, dto.ModelName, dto.Segment });
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id) {
        var sql = "DELETE FROM VehicleModels WHERE ModelId = @id;";
        var rows = await _db.ExecuteAsync(sql, new { id });
        return rows > 0;
    }
}
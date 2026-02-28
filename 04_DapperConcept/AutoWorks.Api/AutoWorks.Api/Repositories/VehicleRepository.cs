
using AutoWorks.Api.DTOs;
using System.Data;
using Dapper;

namespace AutoWorks.Api.Repositories;

public class VehicleRepository 
{
    private readonly IDbConnection _db; 
    public VehicleRepository(IDbConnection db) 
        => _db = db;


    public async Task<IEnumerable<VehicleReadDto>> GetAllAsync(int page = 1, int pageSize = 20) 
    {
        var sql = """ 
            SELECT 
                VehicleId, ModelId, Vin, Color, MfgYear, Price, Status, CreatedAt
            FROM Vehicles
                ORDER BY VehicleId DESC
                    OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;
            """;

        return await _db.QueryAsync<VehicleReadDto>(sql, new {
            offset = (page - 1) * pageSize,
            pageSize 
        });
    } 

    
    public async Task<VehicleReadDto?> GetByIdAsync(int id) 
    {
        var sql = """
            SELECT 
                VehicleId, ModelId, Vin, Color, MfgYear, Price, Status, CreatedAt 
            FROM Vehicles 
            WHERE
                VehicleId = @id;
            """;

        return await _db.QueryFirstOrDefaultAsync<VehicleReadDto>(sql, new { id });
    }


    public async Task<int> CreateAsync(VehicleCreateDto dto) 
    {
        var sql = """
            INSERT INTO Vehicles(ModelId, Vin, Color, MfgYear, Price, Status) 
            OUTPUT INSERTED.VehicleId 
            VALUES(@ModelId, @Vin, @Color, @MfgYear, @Price, @Status);
            """;

        return await _db.ExecuteScalarAsync<int>(sql, dto);
    } 


    public async Task<bool> UpdateAsync(int id, VehicleUpdateDto dto) 
    {
        var sql = """
            UPDATE Vehicles 
            SET
                Color = COALESCE(@Color, Color), 
                Price = COALESCE(@Price, Price), 
                Status = COALESCE(@Status, Status) 
            WHERE VehicleId = @id;
            """;

        var rows = await _db.ExecuteAsync(sql, new { id, dto.Color, dto.Price, dto.Status });

        return rows > 0; 
    }


    public async Task<bool> DeleteAsync(int id) 
    {
        var sql = "DELETE FROM Vehicles WHERE VehicleId = @id;";
        var rows = await _db.ExecuteAsync(sql, new { id }); 

        return rows > 0;
    }
}
using AutoWorks.Api.DTOs;
using System.Data;
using Dapper;

namespace AutoWorks.Api.Repositories;

public class ManufacturerRepository 
{
    private readonly IDbConnection _db;
    public ManufacturerRepository(IDbConnection db) => _db = db;

    public async Task<IEnumerable<ManufacturerReadDto>> GetAllAsync(int page = 1, int pageSize = 20) 
    {
        var sql = """
            SELECT ManufacturerId, CompanyName, Country, CreatedAt
            FROM Manufacturers
            ORDER BY ManufacturerId DESC
            OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;
        """;

        return await _db.QueryAsync<ManufacturerReadDto>(sql, new { offset = (page - 1) * pageSize, pageSize });
    }

    public async Task<ManufacturerReadDto?> GetByIdAsync(int id) 
    {
        var sql = """
            SELECT ManufacturerId, CompanyName, Country, CreatedAt
            FROM Manufacturers
            WHERE ManufacturerId = @id;
        """;

        return await _db.QueryFirstOrDefaultAsync<ManufacturerReadDto>(sql, new { id });
    }

    public async Task<int> CreateAsync(ManufacturerCreateDto dto) 
    {
        var sql = """
            INSERT INTO Manufacturers(CompanyName, Country)
            OUTPUT INSERTED.ManufacturerId
            VALUES(@CompanyName, @Country);
        """;

        return await _db.ExecuteScalarAsync<int>(sql, dto);
    }

    public async Task<bool> UpdateAsync(int id, ManufacturerUpdateDto dto) 
    {
        var sql = """
            UPDATE Manufacturers
            SET CompanyName = COALESCE(@CompanyName, CompanyName),
                Country = COALESCE(@Country, Country)
            WHERE ManufacturerId = @id;
        """;

        var rows = await _db.ExecuteAsync(sql, new { id, dto.CompanyName, dto.Country });
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id) 
    {
        var sql = "DELETE FROM Manufacturers WHERE ManufacturerId = @id;";
        var rows = await _db.ExecuteAsync(sql, new { id });
        return rows > 0;
    }
}

using AutoWorks.Api.DTOs;
using AutoWorks.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AutoWorks.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase 
{
    private readonly VehicleRepository _repo;
    public VehiclesController(VehicleRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleReadDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery][Range(1, 100)] int pageSize = 20)
        => Ok(await _repo.GetAllAsync(page, pageSize));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VehicleReadDto>> GetById(int id) 
    {
        var vehicle = await _repo.GetByIdAsync(id);
        return vehicle is null ? NotFound() : Ok(vehicle);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] VehicleCreateDto dto) 
    {
        var newId = await _repo.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { VehicleId = newId });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] VehicleUpdateDto dto)
        => await _repo.UpdateAsync(id, dto) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
        => await _repo.DeleteAsync(id) ? NoContent() : NotFound();
}
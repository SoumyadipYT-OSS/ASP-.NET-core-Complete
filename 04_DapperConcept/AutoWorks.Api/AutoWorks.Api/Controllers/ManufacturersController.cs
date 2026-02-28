using AutoWorks.Api.DTOs;
using AutoWorks.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoWorks.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ManufacturersController : ControllerBase 
{
    private readonly ManufacturerRepository _repo;
    public ManufacturersController(ManufacturerRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ManufacturerReadDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        => Ok(await _repo.GetAllAsync(page, pageSize));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ManufacturerReadDto>> GetById(int id) {
        var manufacturer = await _repo.GetByIdAsync(id);
        return manufacturer is null ? NotFound() : Ok(manufacturer);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ManufacturerCreateDto dto) {
        var newId = await _repo.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { ManufacturerId = newId });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ManufacturerUpdateDto dto)
        => await _repo.UpdateAsync(id, dto) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
        => await _repo.DeleteAsync(id) ? NoContent() : NotFound();
}
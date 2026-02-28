using AutoWorks.Api.DTOs;
using AutoWorks.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoWorks.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleModelsController : ControllerBase {
    private readonly VehicleModelRepository _repo;
    public VehicleModelsController(VehicleModelRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleModelReadDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        => Ok(await _repo.GetAllAsync(page, pageSize));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VehicleModelReadDto>> GetById(int id) {
        var model = await _repo.GetByIdAsync(id);
        return model is null ? NotFound() : Ok(model);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] VehicleModelCreateDto dto) {
        var newId = await _repo.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { ModelId = newId });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] VehicleModelUpdateDto dto)
        => await _repo.UpdateAsync(id, dto) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
        => await _repo.DeleteAsync(id) ? NoContent() : NotFound();
}
using Microsoft.AspNetCore.Mvc;
using Application.OrdensServico.Services;
using Application.OrdensServico.DTOs;
using Application.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.OrdensServico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VeiculoController : ControllerBase
{
    private readonly IVeiculoService _service;

    public VeiculoController(IVeiculoService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAll());

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
        => Ok(await _service.GetById(id));

    [Authorize(Roles = "Admin")]
    [HttpGet("byPlaca/{placa}")]
    public async Task<IActionResult> GetByPlaca(string placa)
        => Ok(await _service.GetByPlaca(placa));

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VeiculoCreateDto dto)
    {
        await _service.Create(dto);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] VeiculoUpdateDto dto)
    {
        await _service.Update(id, dto);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}

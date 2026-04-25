using Microsoft.AspNetCore.Mvc;
using Application.OrdensServico.Services;
using Application.OrdensServico.DTOs;
using Application.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.OrdensServico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
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
    {
        var cliente = await _service.GetById(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("byNome/{nome}")]
    public async Task<IActionResult> GetByNome(string nome)
    {
        var cliente = await _service.GetByNome(nome);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ClienteCreateDto dto)
    {
        Console.WriteLine($"[POST]");
        await _service.Create(dto);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] ClienteUpdateDto dto)
    {
        await _service.Update(id, dto);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return Ok();
    }
}

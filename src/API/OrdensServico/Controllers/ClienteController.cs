using Microsoft.AspNetCore.Mvc;
using Application.OrdensServico.Services;
using Application.OrdensServico.DTOs;
using Application.OrdensServico.Interfaces;

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

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var cliente = await _service.GetById(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }



[HttpPost]
public async Task<IActionResult> Post([FromBody] ClienteCreateDto dto)
{
	await _service.Create(dto);
	return Ok();
}

[HttpPut("{id}")]
public async Task<IActionResult> Put(Guid id, [FromBody] ClienteUpdateDto dto)
{
    await _service.Update(id, dto);
    return Ok();
}
		
    // [HttpPost]
    // public async Task<IActionResult> Post(string nome, string email)
    // {
    //     await _service.Create(nome, email);
    //     return Ok();
    // }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> Put(Guid id, string nome, string email)
    // {
    //     await _service.Update(id, nome, email);
    //     return Ok();
    // }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return Ok();
    }
}

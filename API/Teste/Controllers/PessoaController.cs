using Microsoft.AspNetCore.Mvc;
using Application.Teste.Services;
using Application.Teste.DTOs;

namespace API.Teste.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly PessoaService _service;

    public PessoaController(PessoaService service)
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
public async Task<IActionResult> Post([FromBody] PessoaDto dto)
{
	await _service.Create(dto);
	return Ok();
}

[HttpPost("AdicionaPedido")]
public async Task<IActionResult> AdicionaPedido([FromBody] PessoaAdicionaPedidoDto dto)
{
	await _service.AdicionaPedido(dto.PessoaId, dto.pedido);
	return Ok();
}

[HttpPut("{id}")]
public async Task<IActionResult> Put(Guid id, [FromBody] PessoaDto dto)
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

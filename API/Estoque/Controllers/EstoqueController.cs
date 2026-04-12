namespace API.Estoque.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Estoque.Services;
using Application.Estoque.DTOs;

[ApiController]
[Route("api/[controller]")]
public class EstoqueController : ControllerBase
{
    private readonly EstoqueService _service;

    public EstoqueController(EstoqueService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var peca = await _service.GetById(id);
        if (peca == null) return NotFound();
        return Ok(peca);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PecaCreateDto dto)
    {
        await _service.Create(dto);
        return Ok();
    }

[HttpPut("{id}")]
public async Task<IActionResult> Put(Guid id, [FromBody] PecaUpdateDto dto)
{
	await _service.Update(id, dto);
	return Ok();
}
		
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return Ok();
    }

}

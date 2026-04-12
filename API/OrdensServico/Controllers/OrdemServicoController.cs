namespace API.OrdensServico.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.OrdensServico.Services;
using Application.OrdensServico.DTOs;

[ApiController]
[Route("api/[controller]")]
public class OrdemServicoController : ControllerBase
{
    private readonly OrdemServicoService _service;

    public OrdemServicoController(OrdemServicoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await _service.GetAll();
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrdemServicoCreateDto dto)
    {
        await _service.Criar(dto);
        return Ok();
    }

    [HttpPost("AdicionaPeca")]
    public async Task<IActionResult> AdicionaPecas([FromBody] OrdemServicoAdicionaPecaDto dto)
    {
        await _service.AdicionarPecas(dto);
        return Ok();
    }
}

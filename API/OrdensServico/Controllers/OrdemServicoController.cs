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
        return Ok(await _service.GetAll());
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

    [HttpPost("AdicionaServico")]
    public async Task<IActionResult> AdicionaServicos([FromBody] OrdemServicoAdicionaServicoDto dto)
    {
        await _service.AdicionarServicos(dto);
        return Ok();
    }

    [HttpPost("EnviarOrcamento")]
    public async Task<IActionResult> EnviarOrcamento([FromBody] OrdemServicoEnviaOrcamentoDto dto)
    {
        await _service.EnviarOrcamento(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(OrdemServicoDeleteDto dto)
    {
        await _service.Deletar(dto);
        return NoContent();
    }
}

namespace API.Metricas.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Metricas.DTOs;
using Application.Metricas.Interfaces;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class MetricasController : ControllerBase
{
    private readonly IMetricaOrdemServicoService _service;

    public MetricasController(IMetricaOrdemServicoService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var metricas = await _service.GetAll();
        return Ok(new {Mensagem = $"obtido todas metricas", Metricas = metricas});
    }

    [Authorize]
    [HttpGet("TempoMedio/{id}")]
    public async Task<IActionResult> TempoMedio(Guid id)
    {
        var dto = new TempoMedioOrdemServicoDto
        {
            OrdemServicoId = id
        };
        var segundos = await _service.TempoMedioOrdemServico(dto);
        return Ok(new {Mensagem = $"Tempo medio de execucao {segundos} segundos", Segundos = segundos});
    }

    [Authorize]
    [HttpGet("TempoTotal/{id}")]
    public async Task<IActionResult> TempoTotal(Guid id)
    {
        var dto = new TempoTotalOrdemServicoDto
        {
            OrdemServicoId = id
        };
        var segundos = await _service.TempoTotalOrdemServico(dto);
        return Ok(new {Mensagem = $"Tempo total de execucao {segundos} segundos", Segundos = segundos});
    }
}

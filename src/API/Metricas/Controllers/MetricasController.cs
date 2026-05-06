namespace Gashu.SistemaMecanica.API.Metricas.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Metricas.DTOs;
using Application.Metricas.Interfaces;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Controller responsável pela consulta de métricas de ordens de serviço
/// </summary>
/// <remarks>
/// Fornece informações como tempo médio entre mudanças de status,
/// tempo total de execução e métricas agregadas das ordens de serviço.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class MetricasController : ControllerBase
{
    private readonly IMetricaOrdemServicoService _service;

    public MetricasController(IMetricaOrdemServicoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Lista todas as métricas registradas
    /// </summary>
    /// <response code="200">Lista de métricas retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var metricas = await _service.GetAll();

        return Ok(new
        {
            Message = "Métricas obtidas com sucesso",
            Data = metricas
        });
    }

    /// <summary>
    /// Calcula o tempo médio entre mudanças de status de uma ordem de serviço
    /// </summary>
    /// <param name="id">Identificador da ordem de serviço</param>
    /// <response code="200">Tempo médio calculado com sucesso</response>
    /// <response code="404">Ordem de serviço não encontrada</response>
    [Authorize]
    [HttpGet("ordemservico/{id}/tempo-medio")]
    public async Task<IActionResult> TempoMedio(Guid id)
    {
        var dto = new TempoMedioOrdemServicoDto
        {
            OrdemServicoId = id
        };

        var segundos = await _service.TempoMedioOrdemServico(dto);

        return Ok(new
        {
            Message = "Tempo médio calculado com sucesso",
            Segundos = segundos
        });
    }

    /// <summary>
    /// Calcula o tempo total de execução de uma ordem de serviço
    /// </summary>
    /// <param name="id">Identificador da ordem de serviço</param>
    /// <response code="200">Tempo total calculado com sucesso</response>
    /// <response code="404">Ordem de serviço não encontrada</response>
    [Authorize]
    [HttpGet("ordemservico/{id}/tempo-total")]
    public async Task<IActionResult> TempoTotal(Guid id)
    {
        var dto = new TempoTotalOrdemServicoDto
        {
            OrdemServicoId = id
        };

        var segundos = await _service.TempoTotalOrdemServico(dto);

        return Ok(new
        {
            Message = "Tempo total calculado com sucesso",
            Segundos = segundos
        });
    }

    /// <summary>
    /// Calcula o tempo médio total de execução de todas as ordens de serviço
    /// </summary>
    /// <response code="200">Tempo médio geral calculado com sucesso</response>
    [Authorize]
    [HttpGet("ordemservico/tempo-medio")]
    public async Task<IActionResult> TempoMedioAtendimentos()
    {
        var segundos = await _service.TempoMedioAtendimentos();

        return Ok(new
        {
            Message = "Tempo médio geral calculado com sucesso",
            Segundos = segundos
        });
    }
}

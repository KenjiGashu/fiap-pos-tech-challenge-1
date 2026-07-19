namespace Gashu.SistemaMecanica.API.Metricas.API;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.Metricas.Controllers;

/// <summary>
/// Controller responsável pela consulta de métricas de ordens de serviço
/// </summary>
/// <remarks>
/// Fornece informações como tempo médio entre mudanças de status,
/// tempo total de execução e métricas agregadas das ordens de serviço.
/// </remarks>
[ApiController]
[Route("api/metricas")]
public class MetricasAPI : ControllerBase
{
    private readonly IMetricasController _controller;
    private readonly ILogger<MetricasAPI> _logger;

    public MetricasAPI(IMetricasController controller, ILogger<MetricasAPI> logger)
    {
        _controller = controller;
        _logger = logger;
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
        try
        {
            var output = await _controller.GetAll();
            return Ok(new
            {
                Message = output.Message,
                Data = output.Metricas
            });
        }
        catch (Exception ex)
        {
            _logger.LogDebug($"[GetAll] Erro interno");
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.TempoMedio(id);

            return Ok(new
            {
                Message = output.Message,
                Segundos = output.Tempo
            });
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[TempoMedio] Erro interno {idOrdemServico}", id);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.TempoTotal(id);

            return Ok(new
            {
                Message = output.Message,
                Segundos = output.Tempo
            });
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[TempoTotal] Erro interno {exceptionMessage}", ex.Message);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Calcula o tempo médio total de execução de todas as ordens de serviço
    /// </summary>
    /// <response code="200">Tempo médio geral calculado com sucesso</response>
    [Authorize]
    [HttpGet("ordemservico/tempo-medio")]
    public async Task<IActionResult> TempoMedioAtendimentos()
    {
        try
        {
            var output = await _controller.TempoMedioAtendimentos();

            return Ok(new
            {
                Message = output.Message,
                Segundos = output.Tempo
            });
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[TempoMedioAtendimentos] Erro interno {exceptionMessage}", ex.Message);
            return StatusCode(500);
        }
    }
}

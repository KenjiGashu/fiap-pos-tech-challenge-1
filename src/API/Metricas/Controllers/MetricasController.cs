namespace Gashu.SistemaMecanica.API.Metricas.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Metricas.DTOs;
using Application.Metricas.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.Metricas.Presenters;


/// <inheritdoc/>
public class MetricasController : IMetricasController
{
    private readonly IMetricaOrdemServicoService _service;
    private readonly IMetricasPresenter _presenter;

    public MetricasController(IMetricaOrdemServicoService service)
    {
        _service = service;
    }

    /// <inheritdoc/>
    public async Task<OutputMetricas> GetAll()
    {
        var metricas = await _service.GetAll();
        return _presenter.Present("Metricas obtidas com sucesso", metricas);
    }

    /// <inheritdoc/>
    public async Task<OutputMetricasSegundos> TempoMedio(Guid id)
    {
        var dto = new TempoMedioOrdemServicoDto
        {
            OrdemServicoId = id
        };

        var segundos = await _service.TempoMedioOrdemServico(dto);
        return _presenter.Present("Tempo medio calculado com sucesso", segundos);
    }

    /// <inheritdoc/>
    public async Task<OutputMetricasSegundos> TempoTotal(Guid id)
    {
        var dto = new TempoTotalOrdemServicoDto
        {
            OrdemServicoId = id
        };

        var segundos = await _service.TempoTotalOrdemServico(dto);
        return _presenter.Present("Tempo total calculado com sucesso", segundos);
    }


    /// <inheritdoc/>
    public async Task<OutputMetricasSegundos> TempoMedioAtendimentos()
    {
        var segundos = await _service.TempoMedioAtendimentos();
        return _presenter.Present("Tempo medio geral calculado com sucesso", segundos);
    }
}

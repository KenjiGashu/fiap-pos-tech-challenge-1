namespace Infrastructure.Metricas.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Metricas.Entities;
using Domain.Metricas.Interfaces;
using Infrastructure.Data;

public class MetricaOrdemServicoRepository : IMetricaOrdemServicoRepository
{
    AppDbContext _context;

    public MetricaOrdemServicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Adicionar(MetricaOrdemServico mos)
    {
        await _context.Metricas.AddAsync(mos);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(MetricaOrdemServico mos)
    {
        _context.Metricas.Update(mos);
        await _context.SaveChangesAsync();
    }

    public async Task<MetricaOrdemServico> ObterPorId(Guid id)
    {
        var metricaOrdemServico = await _context.Metricas.FindAsync(id);
        if(metricaOrdemServico == null)
            throw new Exception("metrica ordem servico inexistente.");
        return metricaOrdemServico;
    }

    public async Task<IEnumerable<MetricaOrdemServico>> ObterPorOrdemServicoId(Guid ordemServicoId)
    {
        var metricaOrdemServico = _context.Metricas.Where(m => m.OrdemServicoId == ordemServicoId);
        if(metricaOrdemServico == null)
            throw new Exception("metrica ordem service inexistente.");

        return metricaOrdemServico;
    }

    public async Task<IEnumerable<MetricaOrdemServico>> ObterTodos()
    {
        return _context.Metricas.ToList();
    }

    public async Task Remover(Guid id)
    {
        var toRemove = await _context.Metricas.FindAsync(id);
        if(toRemove != null)
        {
            _context.Metricas.Remove(toRemove);
            await _context.SaveChangesAsync();
        }
    }
}

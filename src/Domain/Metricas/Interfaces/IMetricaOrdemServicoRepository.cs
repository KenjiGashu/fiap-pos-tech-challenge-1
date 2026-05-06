namespace Gashu.SistemaMecanica.Domain.Metricas.Interfaces;

using Gashu.SistemaMecanica.Domain.Metricas.Entities;

public interface IMetricaOrdemServicoRepository
{
    public Task<IEnumerable<MetricaOrdemServico>> ObterTodos();
    public Task<MetricaOrdemServico> ObterPorId(Guid id);
    public Task<IEnumerable<MetricaOrdemServico>> ObterPorOrdemServicoId(Guid ordemServicoId);
    public Task Adicionar(MetricaOrdemServico mos);
    public Task Atualizar(MetricaOrdemServico mos);
    public Task Remover(Guid id);   
}

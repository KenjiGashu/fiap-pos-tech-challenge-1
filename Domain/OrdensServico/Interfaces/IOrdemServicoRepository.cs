namespace Domain.OrdensServico.Interfaces;

using Domain.OrdensServico.Entities;

public interface IOrdemServicoRepository
{
    Task<OrdemServico> ObterPorId(Guid id);
    Task<IEnumerable<OrdemServico>> ObterTodos();
    Task Adicionar(OrdemServico os);
    Task Atualizar(OrdemServico os);
}

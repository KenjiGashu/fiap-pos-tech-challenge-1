namespace Domain.OrdensServico.Interfaces;

using Domain.OrdensServico.Entities;

public interface IOrdemServicoRepository
{
    Task<OrdemServico> ObterPorId(Guid id);
    Task<IEnumerable<OrdemServico>> ObterTodos();
    Task AdicionarPecas(OrdemServico os);
	  Task AdicionarPecas(Guid id, IEnumerable<OrdemServicoPeca> pecas);
	  Task AdicionarServicos(OrdemServico os);
    Task Atualizar(OrdemServico os);
    Task Deletar(Guid id);
    Task SaveChangesAsync();
}

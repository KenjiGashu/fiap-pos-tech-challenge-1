namespace Gashu.SistemaMecanica.Domain.OrdensServico.Interfaces;

using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public interface IOrdemServicoRepository
{
    Task Criar(OrdemServico os);
    Task<OrdemServico> ObterPorId(Guid id);
    Task<IEnumerable<OrdemServico>> ObterTodos();
    Task<IEnumerable<OrdemServico>> ObterPorIdCliente(Guid clienteId);
    Task AdicionarPecas(OrdemServico os);
    Task AdicionarPecas(Guid id, IEnumerable<OrdemServicoPeca> pecas);
    Task AdicionarServicos(OrdemServico os);
    Task Atualizar(OrdemServico os);
    Task Deletar(Guid id);
    Task SaveChangesAsync();
}

namespace Gashu.SistemaMecanica.Domain.OrdensServico.Interfaces;
using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public interface IServicoRepository
{
    Task<IEnumerable<Servico>> ObterTodos();
    Task<Servico> ObterPorId(Guid id);
    Task Adicionar(Servico servico);
    Task Atualizar(Servico servico);
    Task Remover(Guid id);
}

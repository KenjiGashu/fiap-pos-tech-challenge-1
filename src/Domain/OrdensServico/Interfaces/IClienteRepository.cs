namespace Domain.OrdensServico.Interfaces;
using Domain.OrdensServico.Entities;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObterTodos();
    Task<Cliente?> ObterPorId(Guid id);
    Task Adicionar(Cliente cliente);
    Task Atualizar(Cliente cliente);
    Task Remover(Guid id);
}

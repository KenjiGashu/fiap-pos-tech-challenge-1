namespace Domain.OrdensServico.Interfaces;
using Domain.OrdensServico.Entities;

public interface IVeiculoRepository
{
    Task<IEnumerable<Veiculo>> ObterTodos();
    Task<Veiculo> ObterPorId(Guid id);
    Task Adicionar(Veiculo veiculo);
    Task Atualizar(Veiculo veiculo);
    Task Remover(Guid id);
}

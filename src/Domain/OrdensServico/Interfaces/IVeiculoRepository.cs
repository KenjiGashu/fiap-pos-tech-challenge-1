namespace Gashu.SistemaMecanica.Domain.OrdensServico.Interfaces;
using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public interface IVeiculoRepository
{
    Task<IEnumerable<Veiculo>> ObterTodos();
    Task<Veiculo> ObterPorId(Guid id);
    Task<Veiculo> ObterPorPlaca(string placa);
    Task Adicionar(Veiculo veiculo);
    Task Atualizar(Veiculo veiculo);
    Task Remover(Guid id);
}

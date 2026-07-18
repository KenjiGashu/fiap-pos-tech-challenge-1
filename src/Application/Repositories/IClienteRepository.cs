namespace Gashu.SistemaMecanica.Application.Repositories;
using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObterTodos();
    Task<Cliente?> ObterPorId(Guid id);
    public Task<Cliente?> ObterPorNome(string nome);
    public Task<Cliente?> ObterPorCpf(string cpf);
    public Task<Cliente?> ObterPorCnpj(string cnpj);
    Task Adicionar(Cliente cliente);
    Task Atualizar(Cliente cliente);
    Task Remover(Guid id);
}

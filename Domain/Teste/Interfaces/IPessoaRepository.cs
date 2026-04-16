namespace Domain.Teste.Interfaces;
using Domain.Teste.Entities;

public interface IPessoaRepository
{
    Task<IEnumerable<Pessoa>> ObterTodos();
    Task<IEnumerable<Blog>> ObterTodosBlogs();
    Task<Pessoa> ObterPorId(Guid id);
    Task Adicionar(Pessoa pessoa);
    Task Atualizar(Pessoa pessoa);
    Task Remover(Guid id);
    Task AdicionaPedido(Guid pessoaId, Pedido novoPedido);
    Task AdicionaPedidos(Guid pessoaId, IEnumerable<Pedido> novoPedidos);
}

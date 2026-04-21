namespace Domain.Estoque.Interfaces;
using Domain.Estoque.Entities;

public interface IPecaRepository
{
    Task<IEnumerable<Peca>> ObterTodos();
    Task<Peca?> ObterPorId(Guid id);
    Task Adicionar(Peca peca);
    Task Atualizar(Peca peca);
    Task Remover(Guid id);
    Task SaveChangesAsync();
}

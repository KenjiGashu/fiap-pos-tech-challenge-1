namespace Domain.Identidade.Interfaces;

using Domain.Identidade.Entities;

public interface IUsuarioRepository
{
    public Task<IEnumerable<Usuario>> ObterTodos();
    public Task<Usuario?> ObterPorId(Guid id);
    public Task<Usuario?> ObterPorEmail(string email);
    public Task Adicionar(Usuario usuario);
    public Task Atualizar(Usuario usuario);
    public Task Remover(Guid id);
    public Task SaveChangesAsync();
}

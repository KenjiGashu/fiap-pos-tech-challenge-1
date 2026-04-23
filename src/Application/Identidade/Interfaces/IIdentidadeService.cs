namespace Application.Identidade.Interfaces;

using Domain.Identidade.Entities;
using Application.Identidade.DTOs;

public interface IIdentidadeService
{
    public Task<string> Login(string email, string password);
    public Task CriaUsuario(string email, string password);
    public Task<IEnumerable<ObterTodosResponseDto>> ObterTodos();
    public string HashPassword(string password);
}

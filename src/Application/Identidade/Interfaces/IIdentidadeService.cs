namespace Application.Identidade.Interfaces;

using Domain.Identidade.Entities;
using Application.Identidade.DTOs;

public interface IIdentidadeService
{
    public Task<string> Login(string email, string password);
    public Task CriaUsuario(string email, string password, IEnumerable<string> roles);
    public Task<IEnumerable<UsuarioResponseDto>> ObterTodos();
    public Task<UsuarioResponseDto> ObterPorEmail(string email);
    public string HashPassword(string password);
}

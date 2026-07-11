using System.Text.Json;
using Gashu.SistemaMecanica.Application.Identidade.DTOs;
using Gashu.SistemaMecanica.Domain.Identidade.Entities;

namespace Gashu.SistemaMecanica.API.Identidade.Presenters;

/// <inheritdoc/>
public class IdentidadePresenterDAO : IIdentidadePresenter
{
    /// <inheritdoc/>
    public async Task<OutputIdentidadeToken> Present(string message, string? token)
    {
        return new OutputIdentidadeToken { Message = message , Token = token};
    }
    
    /// <inheritdoc/>
    public async Task<OutputIdentidadeMensagem> Present(string message)
    {
        return new OutputIdentidadeMensagem { Message = message };
    }

    /// <inheritdoc/>
    public async Task<OutputIdentidadeUsuario> Present(string message, Usuario usuario)
    {
        return new OutputIdentidadeUsuario
        {
            Message = message,
            Usuario = new UsuarioResponseDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Password = usuario.PasswordHash,
                Roles = usuario.Roles.Select(r => r.Nome).ToList()
            }
        };
    }

    /// <inheritdoc/>
    public async Task<OutputIdentidadeUsuarios> Present(string message, IEnumerable<Usuario> usuarios)
    {
        var output = new OutputIdentidadeUsuarios
        {
            Message = message,
            Usuarios = usuarios.Select(u => new UsuarioResponseDto
            {
                Id = u.Id,
                Email = u.Email,
                Password = u.PasswordHash,
                Roles = u.Roles.Select(r => r.Nome).ToList()
            })
            //Usuarios = usuarios
        };

        return output;
    }
}

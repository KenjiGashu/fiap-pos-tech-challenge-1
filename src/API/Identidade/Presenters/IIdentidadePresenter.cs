namespace Gashu.SistemaMecanica.API.Identidade.Presenters;

using Gashu.SistemaMecanica.Domain.Identidade.Entities;
using Gashu.SistemaMecanica.Application.Identidade.Services;

/// <summary>
/// Objeto de retorno do presenter
/// </summary>
public class OutputIdentidadeToken
{
    /// <summary>
    /// Mensagem de retorno
    /// </summary>
    public string Message;
    
    /// <summary>
    /// Token de retorno
    /// </summary>
    public string Token;
}

/// <summary>
/// Objeto de retorno do presenter
/// </summary>
public class OutputIdentidadeMensagem
{
    /// <summary>
    /// Mensagem de retorno
    /// </summary>
    public string Message;
}

/// <summary>
/// Objeto de retorno do presenter
/// </summary>
public class OutputIdentidadeUsuario
{
    /// <summary>
    /// Mensagem de retorno
    /// </summary>
    public string Message;

    /// <summary>
    /// Usuario de retorno
    /// </summary>
    public UsuarioResponseDto Usuario;
}

/// <summary>
/// Objeto de retorno do presenter
/// </summary>
public class OutputIdentidadeUsuarios
{
    /// <summary>
    /// Mensagem de retorno
    /// </summary>
    public string Message;

    /// <summary>
    /// Usuario de retorno
    /// </summary>
    public IEnumerable<UsuarioResponseDto> Usuarios;
}

/// <summary>
/// Adaptor que tem a função de Presenter do clean arch
/// </summary>
public interface IIdentidadePresenter
{
    /// <summary>
    /// Converter retorno do controller para string json
    /// </summary>
    public Task<OutputIdentidadeMensagem> Present(string message);
    
    /// <summary>
    /// Converter retorno do controller para string json
    /// </summary>
    public Task<OutputIdentidadeUsuario> Present(string message, Usuario usuario);
    
    /// <summary>
    /// Converter retorno do controller para string json
    /// </summary>
    public Task<OutputIdentidadeUsuarios> Present(string message, IEnumerable<Usuario> usuarios);

    /// <summary>
    /// Converter retorno do controller para string json
    /// </summary>
    public Task<OutputIdentidadeToken> Present(string message, string? token);
}

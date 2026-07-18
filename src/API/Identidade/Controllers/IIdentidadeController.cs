namespace Gashu.SistemaMecanica.API.Identidade.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Estoque.Services;
using Application.Estoque.DTOs;
using Application.Identidade.Interfaces;
using Application.Identidade.DTOs;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.Identidade.Presenters;

/// <summary>
/// Controller responsável pela autenticação e gerenciamento de usuários
/// </summary>
/// <remarks>
/// Permite realizar login, consultar usuários e criar novos usuários no sistema.
/// </remarks>
public interface IIdentidadeController
{
    /// <summary>
    /// Realiza autenticação do usuário
    /// </summary>
    /// <remarks>
    /// Valida as credenciais informadas e retorna um token JWT para acesso aos endpoints protegidos.
    /// </remarks>
    /// <param name="dto">Credenciais de acesso (email e senha)</param>
    public Task<OutputIdentidadeToken> Login([FromBody] LoginDto dto);

    /// <summary>
    /// Lista todos os usuários cadastrados
    /// </summary>
    /// <remarks>
    /// Retorna todos os usuários do sistema.
    /// Requer perfil Admin.
    /// </remarks>
    public Task<OutputIdentidadeUsuarios> ObterTodosOsUsuarios();

    /// <summary>
    /// Obtém um usuário pelo e-mail
    /// </summary>
    /// <param name="email">E-mail do usuário</param>
    public Task<OutputIdentidadeUsuario> ObterPorEmail(string email);

    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <remarks>
    /// Cria um usuário no sistema com os papéis (roles) informados.
    /// Requer perfil Admin.
    /// </remarks>
    /// <param name="dto">Dados do usuário (email, senha e roles)</param>
    public Task<OutputIdentidadeMensagem> CriarUsuario([FromBody] CriarUsuarioDto dto);
}

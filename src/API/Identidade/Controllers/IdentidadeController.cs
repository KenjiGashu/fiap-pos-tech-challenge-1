namespace API.Identidade.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Estoque.Services;
using Application.Estoque.DTOs;
using Application.Estoque.Interfaces;
using Application.Identidade.Interfaces;
using Application.Identidade.DTOs;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Controller responsável pela autenticação e gerenciamento de usuários
/// </summary>
/// <remarks>
/// Permite realizar login, consultar usuários e criar novos usuários no sistema.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class IdentidadeController : ControllerBase
{
    private readonly IIdentidadeService _service;

    public IdentidadeController(IIdentidadeService service)
    {
        _service = service;
    }

    /// <summary>
    /// Realiza autenticação do usuário
    /// </summary>
    /// <remarks>
    /// Valida as credenciais informadas e retorna um token JWT para acesso aos endpoints protegidos.
    /// </remarks>
    /// <param name="dto">Credenciais de acesso (email e senha)</param>
    /// <response code="200">Login realizado com sucesso</response>
    /// <response code="401">Credenciais inválidas</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _service.Login(dto.Email, dto.Password);

        if (token == null)
        {
            return Unauthorized(new { Message = "Credenciais inválidas" });
        }

        return Ok(new
        {
            Message = "Login realizado com sucesso",
            Token = token
        });
    }

    /// <summary>
    /// Lista todos os usuários cadastrados
    /// </summary>
    /// <remarks>
    /// Retorna todos os usuários do sistema.
    /// Requer perfil Admin.
    /// </remarks>
    /// <response code="200">Lista de usuários retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("usuarios")]
    public async Task<IActionResult> ObterTodosOsUsuarios()
    {
        var usuarios = await _service.ObterTodos();

        return Ok(new
        {
            Message = "Usuários obtidos com sucesso",
            Data = usuarios.ToList()
        });
    }

    /// <summary>
    /// Obtém um usuário pelo e-mail
    /// </summary>
    /// <param name="email">E-mail do usuário</param>
    /// <response code="200">Usuário encontrado</response>
    /// <response code="404">Usuário não encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("usuarios/{email}")]
    public async Task<IActionResult> ObterPorEmail(string email)
    {
        var usuario = await _service.ObterPorEmail(email);

        if (usuario == null)
            return NotFound();

        return Ok(new
        {
            Message = "Usuário encontrado",
            Data = usuario
        });
    }

    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <remarks>
    /// Cria um usuário no sistema com os papéis (roles) informados.
    /// Requer perfil Admin.
    /// </remarks>
    /// <param name="dto">Dados do usuário (email, senha e roles)</param>
    /// <response code="200">Usuário criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("usuarios")]
    public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioDto dto)
    {
        await _service.CriaUsuario(dto.Email, dto.Password, dto.Roles);

        return Ok(new
        {
            Message = "Usuário criado com sucesso"
        });
    }
}

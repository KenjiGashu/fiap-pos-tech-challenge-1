namespace Gashu.SistemaMecanica.API.Identidade.API;

using Microsoft.AspNetCore.Mvc;
using Application.Estoque.Services;
using Application.Estoque.DTOs;
using Application.Estoque.Interfaces;
using Application.Identidade.Interfaces;
using Application.Identidade.DTOs;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.Identidade.Controllers;

/// <summary>
/// Controller responsável pela autenticação e gerenciamento de usuários
/// </summary>
/// <remarks>
/// Permite realizar login, consultar usuários e criar novos usuários no sistema.
/// </remarks>
[ApiController]
[Route("api/identidade")]
public class IdentidadeAPI : ControllerBase
{
    private readonly IIdentidadeService _service;
    private readonly IIdentidadeController _controller;

    /// <summary>
    /// Construtor do controller
    /// </summary>
    public IdentidadeAPI(IIdentidadeService service, IIdentidadeController controller)
    {
        _service = service;
        _controller = controller;
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
        var output = await _controller.Login(dto);

        if (output.Token == null)
        {
            return Unauthorized(new { Message = "Credenciais inválidas" });
        }

        return Ok(new
        {
            Message = "Login realizado com sucesso",
            Token = output.Token
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
        try {
            var output = await _controller.ObterTodosOsUsuarios();
            return Ok(new
            {
                Message = "Usuários obtidos com sucesso",
                Data = output.Usuarios.ToList()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
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
        try {
            var output = await _controller.ObterPorEmail(email);

            if (output.Usuario == null)
                return NotFound();
            
            return Ok(new
            {
                Message = "Usuario encontrado",
                Data = output.Usuario
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
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
        try {
            var output = await _controller.CriarUsuario(dto);

            return Ok(new
            {
                Message = "Usuario criado com sucesso",
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}

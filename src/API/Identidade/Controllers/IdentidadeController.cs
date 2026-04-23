namespace API.Identidade.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Estoque.Services;
using Application.Estoque.DTOs;
using Application.Estoque.Interfaces;
using Application.Identidade.Interfaces;
using Application.Identidade.DTOs;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class IdentidadeController : ControllerBase
{
    private readonly IIdentidadeService _service;

    public IdentidadeController(IIdentidadeService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _service.Login(dto.Email, dto.Password);
        return Ok(new {Mensagem = "login efetuado com sucesso", Token = token});
    }

    [HttpGet("usuarios")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ObterTodosOsUsuarios()
    {
        var usuarios = await _service.ObterTodos();
        return Ok(new { Usuarios = usuarios.ToList() });
    }
}

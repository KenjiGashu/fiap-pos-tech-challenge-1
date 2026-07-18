namespace Gashu.SistemaMecanica.API.Identidade.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Identidade.Interfaces;
using Gashu.SistemaMecanica.API.Identidade.Presenters;
using Gashu.SistemaMecanica.Application.Identidade.Services;


/// <inheritdoc/>
public class IdentidadeController : IIdentidadeController
{
    private readonly IIdentidadeService _service;
    private readonly IIdentidadePresenter _presenter;

    /// <summary>
    /// construtor de IdentidadeController
    /// </summary>
    public IdentidadeController(IIdentidadeService service, IIdentidadePresenter presenter)
    {
        _service = service;
        _presenter = presenter;
    }

    /// <inheritdoc/>
    public async Task<OutputIdentidadeToken> Login([FromBody] LoginDto dto)
    {
        var token = await _service.Login(dto.Email, dto.Password);

        return await _presenter.Present("Login realizado com sucesso", token);
    }

    /// <inheritdoc/>
    public async Task<OutputIdentidadeUsuarios> ObterTodosOsUsuarios()
    {
        var usuarios = await _service.ObterTodos();

        return await _presenter.Present("Usuarios obtidos com sucesso", usuarios);
    }

    /// <inheritdoc/>
    public async Task<OutputIdentidadeUsuario> ObterPorEmail(string email)
    {
        var usuario = await _service.ObterPorEmail(email);

        return await _presenter.Present("Usuario encontrado", usuario);
    }

    /// <inheritdoc/>
    public async Task<OutputIdentidadeMensagem> CriarUsuario([FromBody] CriarUsuarioDto dto)
    {
        await _service.CriaUsuario(dto.Email, dto.Password, dto.Roles);

        return await _presenter.Present("Usuario criado com sucesso");
    }
}

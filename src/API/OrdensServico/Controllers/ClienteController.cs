using Microsoft.AspNetCore.Mvc;
using Application.OrdensServico.Services;
using Application.OrdensServico.DTOs;
using Application.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.OrdensServico.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de clientes
/// </summary>
/// <remarks>
/// Permite cadastrar, consultar, atualizar e remover clientes do sistema.
/// Todos os endpoints requerem perfil Admin.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
    {
        _service = service;
    }

    /// <summary>
    /// Lista todos os clientes cadastrados
    /// </summary>
    /// <response code="200">Lista de clientes retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAll());

    /// <summary>
    /// Obtém um cliente pelo ID
    /// </summary>
    /// <param name="id">Identificador do cliente</param>
    /// <response code="200">Cliente encontrado</response>
    /// <response code="404">Cliente não encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var cliente = await _service.GetById(id);

        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    /// <summary>
    /// Busca clientes pelo nome
    /// </summary>
    /// <param name="nome">Nome ou parte do nome do cliente</param>
    /// <response code="200">Clientes encontrados</response>
    /// <response code="404">Nenhum cliente encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("nome/{nome}")]
    public async Task<IActionResult> GetByNome(string nome)
    {
        var cliente = await _service.GetByNome(nome);

        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    /// <summary>
    /// Cria um novo cliente
    /// </summary>
    /// <param name="dto">Dados do cliente a ser criado</param>
    /// <response code="200">Cliente criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ClienteCreateDto dto)
    {
        await _service.Create(dto);
        return Ok(new { Message = "Cliente criado com sucesso!" });
    }

    /// <summary>
    /// Atualiza um cliente existente
    /// </summary>
    /// <param name="id">Identificador do cliente</param>
    /// <param name="dto">Dados atualizados do cliente</param>
    /// <response code="200">Cliente atualizado com sucesso</response>
    /// <response code="404">Cliente não encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] ClienteUpdateDto dto)
    {
        await _service.Update(id, dto);
        return Ok(new { Message = "Cliente atualizado com sucesso!" });
    }

    /// <summary>
    /// Remove um cliente
    /// </summary>
    /// <param name="id">Identificador do cliente</param>
    /// <response code="204">Cliente removido com sucesso</response>
    /// <response code="404">Cliente não encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}

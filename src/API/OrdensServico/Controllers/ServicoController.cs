namespace API.OrdensServico.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.OrdensServico.Services;
using Application.OrdensServico.DTOs;
using Application.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Controller responsável pelo gerenciamento de serviços
/// </summary>
/// <remarks>
/// Permite cadastrar, consultar, atualizar e remover serviços disponíveis
/// para execução em ordens de serviço.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class ServicoController : ControllerBase
{
    private readonly IServicoService _service;

    public ServicoController(IServicoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Lista todos os serviços cadastrados
    /// </summary>
    /// <remarks>
    /// Retorna todos os serviços disponíveis no sistema.
    /// Requer perfil Admin.
    /// </remarks>
    /// <response code="200">Lista de serviços retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAll());

    /// <summary>
    /// Obtém um serviço pelo ID
    /// </summary>
    /// <param name="id">Identificador do serviço</param>
    /// <response code="200">Serviço encontrado</response>
    /// <response code="404">Serviço não encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var servico = await _service.GetById(id);

        if (servico == null)
            return NotFound();

        return Ok(servico);
    }

    /// <summary>
    /// Cria um novo serviço
    /// </summary>
    /// <param name="dto">Dados do serviço a ser criado</param>
    /// <response code="200">Serviço criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ServicoCreateDto dto)
    {
        await _service.Create(dto);
        return Ok(new { Message = "Serviço criado com sucesso!" });
    }

    /// <summary>
    /// Atualiza um serviço existente
    /// </summary>
    /// <param name="id">Identificador do serviço</param>
    /// <param name="dto">Dados atualizados do serviço</param>
    /// <response code="200">Serviço atualizado com sucesso</response>
    /// <response code="404">Serviço não encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] ServicoUpdateDto dto)
    {
        await _service.Update(id, dto);
        return Ok(new { Message = "Serviço atualizado com sucesso!" });
    }

    /// <summary>
    /// Remove um serviço
    /// </summary>
    /// <param name="id">Identificador do serviço</param>
    /// <response code="204">Serviço removido com sucesso</response>
    /// <response code="404">Serviço não encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}

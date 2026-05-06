namespace Gashu.SistemaMecanica.API.Estoque.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Estoque.Services;
using Application.Estoque.DTOs;
using Application.Estoque.Interfaces;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Controller responsável pelo gerenciamento de estoque
/// </summary>
/// <remarks>
/// Este contexto é responsável pelo controle de peças disponíveis em estoque,
/// incluindo cadastro, consulta, atualização e remoção.
/// Todos os endpoints requerem perfil Admin.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class EstoqueController : ControllerBase
{
    private readonly IEstoqueService _service;

    public EstoqueController(IEstoqueService service)
    {
        _service = service;
    }

    /// <summary>
    /// Lista todas as peças disponíveis no estoque
    /// </summary>
    /// <response code="200">Lista de peças retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAll());

    /// <summary>
    /// Obtém uma peça do estoque pelo ID
    /// </summary>
    /// <param name="id">Identificador da peça</param>
    /// <response code="200">Peça encontrada</response>
    /// <response code="404">Peça não encontrada</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        Console.WriteLine($"[Get] --------------- {id} --------------");

        var peca = await _service.GetById(id);

        if (peca == null)
            return NotFound();

        return Ok(peca);
    }

    /// <summary>
    /// Cadastra uma nova peça no estoque
    /// </summary>
    /// <param name="dto">Dados da peça a ser cadastrada</param>
    /// <response code="200">Peça cadastrada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PecaCreateDto dto)
    {
        await _service.Create(dto);

        return Ok(new
        {
            Message = "Peça cadastrada com sucesso!"
        });
    }

    /// <summary>
    /// Atualiza uma peça existente no estoque
    /// </summary>
    /// <param name="id">Identificador da peça</param>
    /// <param name="dto">Dados atualizados da peça</param>
    /// <response code="200">Peça atualizada com sucesso</response>
    /// <response code="404">Peça não encontrada</response>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] PecaUpdateDto dto)
    {
        await _service.Update(id, dto);

        return Ok(new
        {
            Message = "Peça atualizada com sucesso!"
        });
    }

    /// <summary>
    /// Remove uma peça do estoque
    /// </summary>
    /// <param name="id">Identificador da peça</param>
    /// <response code="204">Peça removida com sucesso</response>
    /// <response code="404">Peça não encontrada</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);

        return NoContent();
    }
}

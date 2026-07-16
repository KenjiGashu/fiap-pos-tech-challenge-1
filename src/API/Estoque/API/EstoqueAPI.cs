namespace Gashu.SistemaMecanica.API.Estoque.API;

using Microsoft.AspNetCore.Mvc;
using Application.Estoque.Services;
using Application.Estoque.DTOs;
using Application.Estoque.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.Estoque.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de estoque
/// </summary>
/// <remarks>
/// Este contexto é responsável pelo controle de peças disponíveis em estoque,
/// incluindo cadastro, consulta, atualização e remoção.
/// Todos os endpoints requerem perfil Admin.
/// </remarks>
[ApiController]
[Route("api/estoque")]
public class EstoqueAPI : ControllerBase
{
    private readonly IEstoqueController _controller;
    
    /// <summary>
    /// construtor de EstoqueAPI
    /// </summary>
    /// <param name="controller">Controller de Estoque que a API vai direcionar a chamada</param>
    public EstoqueAPI(IEstoqueController controller) => _controller = controller;

    /// <summary>
    /// Lista todas as peças disponíveis no estoque
    /// </summary>
    /// <response code="200">Lista de peças retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try {
            var output = await _controller.Get();
            return Ok(output.pecas);
        } catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
    

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
        try
        {
            var output = await _controller.GetById(id);

            if (output.peca == null)
                return NotFound(output.message);
            
            return Ok(output.peca);
        } catch (Exception ex)
        {
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.Create(dto);

            return Ok(output);
        } catch (Exception ex)
        {
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.Update(id, dto);

            return Ok(output);
        } catch (Exception ex)
        {
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.Delete(id);

            return Ok(output);
        } catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}

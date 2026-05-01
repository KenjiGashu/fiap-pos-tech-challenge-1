using Microsoft.AspNetCore.Mvc;
using Application.OrdensServico.Services;
using Application.OrdensServico.DTOs;
using Application.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.OrdensServico.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de veículos
/// </summary>
/// <remarks>
/// Permite cadastrar, consultar, atualizar e remover veículos
/// vinculados às ordens de serviço.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class VeiculoController : ControllerBase
{
    private readonly IVeiculoService _service;

    public VeiculoController(IVeiculoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Lista todos os veículos cadastrados
    /// </summary>
    /// <remarks>
    /// Retorna todos os veículos registrados no sistema.
    /// Requer perfil Admin.
    /// </remarks>
    /// <response code="200">Lista de veículos retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAll());

    /// <summary>
    /// Obtém um veículo pelo ID
    /// </summary>
    /// <param name="id">Identificador do veículo</param>
    /// <response code="200">Veículo encontrado</response>
    /// <response code="404">Veículo não encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var veiculo = await _service.GetById(id);

        if (veiculo == null)
            return NotFound();

        return Ok(veiculo);
    }

    /// <summary>
    /// Busca um veículo pela placa
    /// </summary>
    /// <param name="placa">Placa do veículo (ex: ABC1234)</param>
    /// <response code="200">Veículo encontrado</response>
    /// <response code="404">Veículo não encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("byPlaca/{placa}")]
    public async Task<IActionResult> GetByPlaca(string placa)
    {
        var veiculo = await _service.GetByPlaca(placa);

        if (veiculo == null)
            return NotFound();

        return Ok(veiculo);
    }

    /// <summary>
    /// Cria um novo veículo
    /// </summary>
    /// <param name="dto">Dados do veículo a ser criado</param>
    /// <response code="200">Veículo criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VeiculoCreateDto dto)
    {
        await _service.Create(dto);
        return Ok(new { Message = "Veículo criado com sucesso!" });
    }

    /// <summary>
    /// Atualiza um veículo existente
    /// </summary>
    /// <param name="id">Identificador do veículo</param>
    /// <param name="dto">Dados atualizados do veículo</param>
    /// <response code="200">Veículo atualizado com sucesso</response>
    /// <response code="404">Veículo não encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] VeiculoUpdateDto dto)
    {
        await _service.Update(id, dto);
        return Ok(new { Message = "Veículo atualizado com sucesso!" });
    }

    /// <summary>
    /// Remove um veículo
    /// </summary>
    /// <param name="id">Identificador do veículo</param>
    /// <response code="204">Veículo removido com sucesso</response>
    /// <response code="404">Veículo não encontrado</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}

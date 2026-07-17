using Microsoft.AspNetCore.Mvc;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.OrdensServico.Controllers;

namespace Gashu.SistemaMecanica.API.OrdensServico.API;

/// <summary>
/// Controller responsável pelo gerenciamento de veículos
/// </summary>
/// <remarks>
/// Permite cadastrar, consultar, atualizar e remover veículos
/// vinculados às ordens de serviço.
/// </remarks>
[ApiController]
[Route("api/veiculo")]
public class VeiculoAPI : ControllerBase
{
    private readonly IVeiculoController _controller;

    public VeiculoAPI(IVeiculoController controller)
    {
        _controller = controller;
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
    {
        try
        {
            var output = await _controller.GetAll();
            return Ok(output.Veiculos);
        }
        catch (Exception ex)
        {
	          Console.WriteLine(ex.ToString());
            return StatusCode(500);
        }
    }

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
        try
        {
            var output = await _controller.GetById(id);

            if (output.Veiculo == null)
                return NotFound();

            return Ok(output.Veiculo);
        }
        catch (Exception ex)
        {
	          Console.WriteLine(ex.ToString());
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.GetByPlaca(placa);

            if (output.Veiculo == null)
                return NotFound();

            return Ok(output.Veiculo);
        }
        catch (Exception ex)
        {
	          Console.WriteLine(ex.ToString());
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.Create(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
	          Console.WriteLine(ex.ToString());
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.Update(id, dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
	          Console.WriteLine(ex.ToString());
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.Delete(id);
            return Ok(output);
        }
        catch (Exception ex)
        {
	          Console.WriteLine(ex.ToString());
            return StatusCode(500);
        }
    }
}

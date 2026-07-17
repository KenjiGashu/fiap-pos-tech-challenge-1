using Microsoft.AspNetCore.Mvc;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.OrdensServico.Controllers;

namespace Gashu.SistemaMecanica.API.OrdensServico.API;

/// <summary>
/// Controller responsável pelo gerenciamento de clientes
/// </summary>
/// <remarks>
/// Permite cadastrar, consultar, atualizar e remover clientes do sistema.
/// Todos os endpoints requerem perfil Admin.
/// </remarks>
[ApiController]
[Route("api/cliente")]
public class ClienteAPI : ControllerBase
{
    private readonly IClienteController _controller;

    public ClienteAPI(IClienteController controller)
    {
        _controller = controller;
    }

    /// <summary>
    /// Lista todos os clientes cadastrados
    /// </summary>
    /// <response code="200">Lista de clientes retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var output = await _controller.Get();
            return Ok(output.Clientes);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
    
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
        try
        {
	          var output = await _controller.Get(id);

            if (output.Cliente == null)
                return NotFound();

            return Ok(output.Cliente);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.GetByNome(nome);

            if (output.Cliente == null)
                return NotFound();

            return Ok(output.Cliente);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.CriaCliente(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.AtualizaCliente(id, dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.DeletaCliente(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}

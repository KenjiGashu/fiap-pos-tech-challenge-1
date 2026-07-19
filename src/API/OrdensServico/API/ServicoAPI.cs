namespace Gashu.SistemaMecanica.API.OrdensServico.API;

using Microsoft.AspNetCore.Mvc;
using Gashu.SistemaMecanica.Application.OrdensServico.Services;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.OrdensServico.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de serviços
/// </summary>
/// <remarks>
/// Permite cadastrar, consultar, atualizar e remover serviços disponíveis
/// para execução em ordens de serviço.
/// </remarks>
[ApiController]
[Route("api/servico")]
public class ServicoAPI : ControllerBase
{
    private readonly IServicoController _controller;
    private readonly ILogger<ServicoAPI> _logger;

    public ServicoAPI(IServicoController controller, ILogger<ServicoAPI> logger)
    {
        _controller = controller;
        _logger = logger;
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
    {
        try
        {
            var output = await _controller.GetAll();
            return Ok(output.Servicos);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{ServicoAPI}] erro ao obter servicos. {ex.Message}. {ex.StackTrace}", "ServicoAPI", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
    }

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
        try
        {
            var output = await _controller.GetById(id);

            if(output.Servico == null)
                return NotFound();

            return Ok(output.Servico);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{ServicoAPI}] erro ao obter servico id[{ServicoId}]. {ex.Message}. {ex.StackTrace}", "ServicoAPI", id, ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.Create(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{ServicoAPI}] erro ao criar servico. {ex.Message}. {ex.StackTrace}", "ServicoAPI", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.Update(id, dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{ServicoAPI}] erro ao atualizar servico. {ex.Message}. {ex.StackTrace}", "ServicoAPI", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
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
        try
        {
            var output = await _controller.Delete(id);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("[{ServicoAPI}]erro ao deletar servico. {ex.Message}. {ex.StackTrace}", "ServicoAPI", ex.Message, ex.StackTrace);
            return StatusCode(500);
        }
    }
}

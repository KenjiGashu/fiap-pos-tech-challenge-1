namespace Gashu.SistemaMecanica.API.OrdensServico.API;

using Microsoft.AspNetCore.Mvc;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.OrdensServico.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de Ordens de Serviço
/// </summary>
[ApiController]
[Route("api/OrdemServico")]
public class OrdemServicoAPI : ControllerBase
{
    private readonly IOrdemServicoController _controller;

    public OrdemServicoAPI(IOrdemServicoController controller)
    {
        _controller = controller;
    }

    /// <summary>
    /// Lista todas as ordens de serviço
    /// </summary>
    /// <remarks>
    /// Retorna todas as ordens cadastradas no sistema.
    /// Requer perfil Admin.
    /// </remarks>
    /// <response code="200">Lista de ordens retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var output = await _controller.GetAll();
            return Ok(output.OrdemServicos);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][Get] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Mostra lista ordens de serviço filtradas e ordenadas
    /// </summary>
    /// <remarks>
    /// Retorna todas as ordens cadastradas no sistema.
    /// Requer perfil Admin.
    /// </remarks>
    /// <response code="200">Lista de ordens retornada com sucesso</response>
    /// <response code="401">Usuário não autorizado</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("ListaOrdensServicos")]
    public async Task<IActionResult> ListaOrdensServicos()
    {
        try
        {
            var output = await _controller.ListaOrdensServicos();
            return Ok(output.OrdemServicos);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][ListaOrdensServicos] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }


    /// <summary>
    /// Obtém uma ordem de serviço por ID
    /// </summary>
    /// <param name="id">Identificador da ordem de serviço</param>
    /// <response code="200">Ordem encontrada</response>
    /// <response code="404">Ordem não encontrada</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        try
        {
            var output = await _controller.GetById(id);
            return Ok(output.OrdemServico);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][GetById] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Busca ordens de serviço por cliente
    /// </summary>
    /// <param name="id">Identificador do cliente</param>
    /// <response code="200">Ordens encontradas</response>
    [HttpGet("cliente/{id}")]
    public async Task<IActionResult> ObterPorIdCliente(Guid id)
    {
        try
        {
            var output = await _controller.GetByClientId(id);
            return Ok(output.OrdemServicos);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][GetByClientId] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Cria uma nova ordem de serviço
    /// </summary>
    /// <param name="dto">Dados da ordem (cliente e veículo)</param>
    /// <response code="200">Ordem criada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrdemServicoCreateDto dto)
    {
        try
        {
            var output = await _controller.Create(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][GetByClientId] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Cria uma nova ordem de serviço
    /// </summary>
    /// <param name="dto">Dados da ordem (cliente e veículo)</param>
    /// <response code="200">Ordem criada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("criarOrdemServico")]
    public async Task<IActionResult> CriarComTodosOsDados([FromBody] OrdemServicoCreateDtoTodosDados dto)
    {
        try
        {
            var output = await _controller.CriarComTodosOsDados(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][CriarComTodosOsDados] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Adiciona peças a uma ordem de serviço
    /// </summary>
    /// <param name="dto">Dados das peças a serem adicionadas</param>
    /// <response code="200">Peças adicionadas com sucesso</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("AdicionaPeca")]
    public async Task<IActionResult> AdicionaPecas([FromBody] OrdemServicoAdicionaPecaDto dto)
    {
        try
        {
            var output = await _controller.AdicionaPecas(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][AdicionaPecas] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Adiciona serviços a uma ordem de serviço
    /// </summary>
    /// <param name="dto">Dados dos serviços</param>
    /// <response code="200">Serviço adicionado com sucesso</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("AdicionaServico")]
    public async Task<IActionResult> AdicionaServicos([FromBody] OrdemServicoAdicionaServicoDto dto)
    {
        try
        {
            var output = await _controller.AdicionaServicos(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][AdicionaServico] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Envia orçamento para aprovação do cliente.
    /// No momento, implementado apenas com email
    /// </summary>
    /// <param name="dto">Dados do envio do orçamento</param>
    /// <response code="200">Orçamento enviado com sucesso</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("EnviarOrcamento")]
    public async Task<IActionResult> EnviarOrcamento([FromBody] OrdemServicoEnviarOrcamentoDto dto)
    {
        try
        {
            var output = await _controller.EnviarOrcamento(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][EnviarOrcamento] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
        // await _controller.EnviarOrcamento(dto);
        // return Ok(new { Message = "Orçamento enviado!" });
    }

    /// <summary>
    /// Aprova um orçamento
    /// </summary>
    /// <param name="id">Token de aprovação</param>
    /// <response code="200">Orçamento aprovado</response>
    [HttpGet("AprovarOrcamento/{id}")]
    public async Task<IActionResult> AprovarOrcamento(Guid id)
    {
        try
        {
            var output = await _controller.AprovarOrcamento(id);
            return Ok(new { Message = output.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][AprovarOrcamento] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Rejeita um orçamento
    /// </summary>
    /// <param name="id">Token de rejeição</param>
    /// <response code="200">Orçamento rejeitado</response>
    [HttpGet("RejeitarOrcamento/{id}")]
    public async Task<IActionResult> RejeitarOrcamento(Guid id)
    {
        try
        {
            var output = await _controller.RejeitarOrcamento(id);
            return Ok(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][RejeitarOrcamento] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Inicia o diagnóstico da ordem
    /// </summary>
    /// <param name="dto">Dados do diagnóstico</param>
    /// <response code="200">Diagnóstico iniciado</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("IniciarDiagnostico")]
    public async Task<IActionResult> IniciarDiagnostico([FromBody] OrdemServicoIniciarDiagnosticoOrcamentoDto dto)
    {
        try
        {
            var output = await _controller.IniciarDiagnostico(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][IniciarDiagnostico] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Finaliza o diagnóstico e envia para aprovação
    /// </summary>
    /// <param name="dto">Dados do diagnóstico</param>
    /// <response code="200">Diagnóstico finalizado</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("FinalizarDiagnostico")]
    public async Task<IActionResult> FinalizarDiagnostico([FromBody] OrdemServicoFinalizarDiagnosticoOrcamentoDto dto)
    {
        try
        {
            var output = await _controller.FinalizarDiagnostico(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][FinalizarDiagnostico] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Inicia execução do serviço
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("IniciarExecucao")]
    public async Task<IActionResult> IniciarExecucao([FromBody] OrdemServicoIniciarExecucaoOrcamentoDto dto)
    {
        try
        {
            var output = await _controller.IniciarExecucao(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][IniciarExecucao] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Finaliza execução do serviço
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("FinalizarExecucao")]
    public async Task<IActionResult> FinalizarExecucao([FromBody] OrdemServicoFinalizarExecucaoOrcamentoDto dto)
    {
        try
        {
            var output = await _controller.FinalizarExecucao(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][FinalizarExecucao] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Realiza entrega do veículo ao cliente
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("EntregarVeiculo")]
    public async Task<IActionResult> EntregarVeiculo([FromBody] OrdemServicoEntregarVeiculoDto dto)
    {
        try
        {
            var output = await _controller.EntregarVeiculo(dto);
            return Ok(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][EntregarVeiculo] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Remove uma ordem de serviço
    /// </summary>
    /// <param name="dto">Dados da ordem a ser removida</param>
    /// <response code="204">Ordem removida com sucesso</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] OrdemServicoDeleteDto dto)
    {
        try
        {
            var output = await _controller.Delete(dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[OrdemServico][Delete] ex {ex.Message}. {ex.StackTrace}");
            return StatusCode(500);
        }
    }
}

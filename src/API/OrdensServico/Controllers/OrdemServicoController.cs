namespace API.OrdensServico.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.OrdensServico.DTOs;
using Application.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class OrdemServicoController : ControllerBase
{
    private readonly IOrdemServicoService _service;

    public OrdemServicoController(IOrdemServicoService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        return Ok(await _service.ObterPorId(id));
    }

    [HttpGet("cliente/{id}")]
    public async Task<IActionResult> ObterPorIdCliente(Guid id)
    {
        Console.WriteLine($"[controller] {id}");
        return Ok(await _service.ObterPorIdCliente(id));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrdemServicoCreateDto dto)
    {
        await _service.Criar(dto);
        return Ok(new {Message = $"criado Servico!"});
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("AdicionaPeca")]
    public async Task<IActionResult> AdicionaPecas([FromBody] OrdemServicoAdicionaPecaDto dto)
    {
        await _service.AdicionarPecas(dto);
        return Ok(new {Message = $"Peca Adicionada!"});
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("AdicionaServico")]
    public async Task<IActionResult> AdicionaServicos([FromBody] OrdemServicoAdicionaServicoDto dto)
    {
        await _service.AdicionarServicos(dto);
        return Ok(new {Message = $"Servico Adicionado!"});
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("EnviarOrcamento")]
    public async Task<IActionResult> EnviarOrcamento([FromBody] OrdemServicoEnviarOrcamentoDto dto)
    {
        await _service.EnviarOrcamento(dto);
        return Ok(new {Message = $"orcamento enviado!"});
    }

    [HttpGet("AprovarOrcamento/{id}")]
    public async Task<IActionResult> AprovarOrcamento(Guid id)
    {
        await _service.AprovarOrcamento(new OrdemServicoAprovarOrcamentoDto{TokenGuid = id});
        return Ok(new {Message = $"Orcamento {id} aprovado com sucesso"});
    }

    [HttpGet("RejeitarOrcamento/{id}")]
    public async Task<IActionResult> RejeitarOrcamento(Guid id)
    {
        await _service.RejeitarOrcamento(new OrdemServicoRejeitarOrcamentoDto{TokenGuid = id});
        return Ok(new {Message = $"Orcamento {id} rejeitado. ):"});
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("IniciarDiagnostico")]
    public async Task<IActionResult> IniciarDiagnostico([FromBody] OrdemServicoIniciarDiagnosticoOrcamentoDto dto)
    {
        await _service.IniciarDiagnostico(dto);
        return Ok(new {Message = $"iniciado diagnostico!"});
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("FinalizarDiagnostico")]
    public async Task<IActionResult> FinalizarDiagnostico([FromBody] OrdemServicoFinalizarDiagnosticoOrcamentoDto dto)
    {
        await _service.FinalizarDiagnostico(dto);
        return Ok(new {Message = $"finalizado diagnostico! Enviado orçamento para Aprovação da Revisao"});
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("IniciarExecucao")]
    public async Task<IActionResult> IniciarExecucao([FromBody] OrdemServicoIniciarExecucaoOrcamentoDto dto)
    {
        await _service.IniciarExecucao(dto);
        return Ok(new {Message = $"iniciado execucao!"});
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("FinalizarExecucao")]
    public async Task<IActionResult> FinalizarExecucao([FromBody] OrdemServicoFinalizarExecucaoOrcamentoDto dto)
    {
        await _service.FinalizarExecucao(dto);
        return Ok(new {Message = $"Finalizado execucao!"});
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("EntregarVeiculo")]
    public async Task<IActionResult> EntregarVeiculo([FromBody] OrdemServicoEntregarVeiculoDto dto)
    {
        await _service.EntregarVeiculo(dto);
        return Ok(new {Message = $"Entrega de Veiculo Feita!"});
    }



    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(OrdemServicoDeleteDto dto)
    {
        await _service.Deletar(dto);
        return NoContent();
    }
}

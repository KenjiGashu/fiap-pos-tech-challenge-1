namespace Gashu.SistemaMecanica.API.OrdensServico.Controllers;

using Microsoft.AspNetCore.Mvc;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Gashu.SistemaMecanica.API.OrdensServico.Presenters;

public class OrdemServicoController : IOrdemServicoController
{
    private readonly IOrdemServicoService _service;
    private readonly IOrdemServicoPresenter _presenter;
    private readonly ILogger<OrdemServicoController> _logger;

    public OrdemServicoController(IOrdemServicoService service, IOrdemServicoPresenter presenter, ILogger<OrdemServicoController> logger)
    {
        _service = service;
        _presenter = presenter;
        _logger = logger;
    }

    public async Task<OutputOrdemServicos> GetAll()
    {
        var ordemServicos = await _service.GetAll();
        return _presenter.Present("Ordens de Servico obtidos com sucesso", ordemServicos);
    }

    public async Task<OutputOrdemServicos> ListaOrdensServicos()
    {
        var ordemServicos = await _service.ListaOrdensServicos();
        return _presenter.Present("Ordens de Servico obtidos com sucesso", ordemServicos);
    }

    public async Task<OutputOrdemServico> GetById(Guid id)
    {
        var ordemServico = await _service.ObterPorId(id);
        return _presenter.Present("Ordem de Servico obtido com sucesso", ordemServico);
    }

    public async Task<OutputOrdemServicos> GetByClientId(Guid id)
    {
        var ordemServicos = await _service.ObterPorIdCliente(id);
        return _presenter.Present("Ordem servico obtido com sucesso.", ordemServicos.OrdemServicos);
    }

    public async Task<OutputMessageOrdemServico> Create(OrdemServicoCreateDto dto)
    {
        await _service.Criar(dto);
        return _presenter.Present("Ordem Servico criado com sucesso!");
    }

    public async Task<OutputMessageOrdemServico> CriarComTodosOsDados(OrdemServicoCreateDtoTodosDados dto)
    {
        await _service.CriarComTodosOsDados(dto);
        return _presenter.Present("Ordem Serviço criado com sucesso!");
    }

    public async Task<OutputMessageOrdemServico> AdicionaPecas(OrdemServicoAdicionaPecaDto dto)
    {
        await _service.AdicionarPecas(dto);
        return _presenter.Present("Peça adicionada!");
    }

    public async Task<OutputMessageOrdemServico> AdicionaServicos(OrdemServicoAdicionaServicoDto dto)
    {
        await _service.AdicionarServicos(dto);
        return _presenter.Present("Serviço adicionado!");
    }

    public async Task<OutputMessageOrdemServico> EnviarOrcamento(OrdemServicoEnviarOrcamentoDto dto)
    {
        await _service.EnviarOrcamento(dto);
        return _presenter.Present("Orçamento enviado!");
    }

    public async Task<OutputMessageOrdemServico> AprovarOrcamento(Guid id)
    {
        await _service.AprovarOrcamento(new OrdemServicoAprovarOrcamentoDto { TokenGuid = id });

        var output = _presenter.Present($"Orçamento {id} aprovado com sucesso");

        _logger.LogDebug("output: {OrdemServicoId} {output.Message}", id, output.Message);
        return output;
    }

    public async Task<OutputMessageOrdemServico> RejeitarOrcamento(Guid id)
    {
        await _service.RejeitarOrcamento(new OrdemServicoRejeitarOrcamentoDto { TokenGuid = id });
        return _presenter.Present($"Orçamento {id} rejeitado");
    }

    public async Task<OutputMessageOrdemServico> IniciarDiagnostico(OrdemServicoIniciarDiagnosticoOrcamentoDto dto)
    {
        await _service.IniciarDiagnostico(dto);
        return _presenter.Present("Diagnóstico iniciado!");
    }

    public async Task<OutputMessageOrdemServico> FinalizarDiagnostico(OrdemServicoFinalizarDiagnosticoOrcamentoDto dto)
    {
        await _service.FinalizarDiagnostico(dto);
        return _presenter.Present("Diagnóstico finalizado e orçamento enviado!");
    }

    public async Task<OutputMessageOrdemServico> IniciarExecucao(OrdemServicoIniciarExecucaoOrcamentoDto dto)
    {
        await _service.IniciarExecucao(dto);
        return _presenter.Present("Execução iniciada!");
    }

    public async Task<OutputMessageOrdemServico> FinalizarExecucao(OrdemServicoFinalizarExecucaoOrcamentoDto dto)
    {
        await _service.FinalizarExecucao(dto);
        return _presenter.Present("Execução finalizada!");
    }

    public async Task<OutputMessageOrdemServico> EntregarVeiculo(OrdemServicoEntregarVeiculoDto dto)
    {
        await _service.EntregarVeiculo(dto);
        return _presenter.Present("Veículo entregue!");
    }

    public async Task<OutputMessageOrdemServico> Delete(OrdemServicoDeleteDto dto)
    {
        await _service.Deletar(dto);
        return _presenter.Present("Peca deletada com sucesso!");
    }
}

namespace Application.OrdensServico.Services;

using Domain.OrdensServico.Interfaces;
using Application.OrdensServico.DTOs;
using Domain.OrdensServico.Entities;
using Domain.Estoque.Interfaces;
using Application.Estoque.DTOs;
using Domain.Estoque.Entities;
using Application.Estoque.Interfaces;

public class OrdemServicoService
{
    private readonly IOrdemServicoRepository _repo;
    private readonly IServicoRepository _servicoRepo;
    private readonly IPecaRepository _pecaRepo;
    private readonly IEstoqueService _estoqueService;

    public OrdemServicoService(
        IOrdemServicoRepository repo,
        IServicoRepository servicoRepo,
        IPecaRepository pecaRepo,
			  IEstoqueService estoqueService)
    {
        _repo = repo;
        _servicoRepo = servicoRepo;
        _pecaRepo = pecaRepo;
        _estoqueService = estoqueService;
    }

    public async Task<IEnumerable<OrdemServicoResponseDto>> GetAll()
	  {
    var ordemServicos = await _repo.ObterTodos();

    return ordemServicos.Select(os => new OrdemServicoResponseDto
    {
        Id = os.Id,
        ClienteId = os.ClienteId,
        VeiculoId = os.VeiculoId,
				Servicos = os.Servicos.Select(s => new OrdemServicoServicoDto
				{
					Id = s.ServicoId,
					Preco = s.Preco
				}).ToList(),
				Pecas = os.Pecas.Select(p => new OrdemServicoPecaDto
				{
					Id = p.PecaId,
					Preco = p.Preco
				}).ToList()
    });
	}

		public async Task Criar(OrdemServicoCreateDto dto)
	  {
        var ordemServico = new OrdemServico(dto.ClienteId, dto.VeiculoId);

        ordemServico.Status = StatusOrdemServico.Recebida;

        await _repo.Adicionar(ordemServico);
    }

		public async Task AdicionarPecas(OrdemServicoAdicionaPecaDto dto)
	  {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);

				foreach (var peca in dto.Pecas){
            ordemServico.AdicionarPeca(peca.Id, peca.Preco, peca.Quantidade);
        }

        await _repo.Atualizar(ordemServico);
    }

		public async Task AdicionarServicos(OrdemServicoAdicionaServicoDto dto)
	  {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);

				foreach (var servico in dto.Servicos)
				{
            ordemServico.AdicionarServico(servico.Id, servico.Preco);
        }

        await _repo.Atualizar(ordemServico);
    }

	  public async Task AprovarOrcamento(OrdemServicoAtualizaStatusDto dto)
	  {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);

        ordemServico.AprovarOrcamento();
    }

		public async Task IniciarDiagnostico(OrdemServicoAtualizaStatusDto dto)
	  {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);

        ordemServico.IniciarDiagnostico();
    }
	
    public async Task IniciarExecucao(OrdemServicoAtualizaStatusDto dto)
	  {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);

				foreach (var peca in ordemServico.Pecas)
				{
            _estoqueService.Consumir(peca.Id, peca.Quantidade);
        }
				
        ordemServico.IniciarExecucao();
    }

    // public async Task Criar(OrdemServicoCreateDto dto)
    // {
    //     var os = new OrdemServico(dto.ClienteId, dto.VeiculoId);

    //     // Serviços
    //     foreach (var servicoDto in dto.Servicos)
    //     {
    //         var servico = await _servicoRepo.ObterPorId(servicoDto.ServicoId);
    //         os.AdicionarServico(servico.Id, servico.Preco);
    //     }

    //     // Peças
    //     foreach (var item in dto.Pecas)
    //     {
    //         var peca = await _pecaRepo.ObterPorId(item.PecaId);
    //         os.AdicionarPeca(peca.Id, peca.Preco, item.Quantidade);
    //     }

    //     await _repo.Adicionar(os);
    // }
}

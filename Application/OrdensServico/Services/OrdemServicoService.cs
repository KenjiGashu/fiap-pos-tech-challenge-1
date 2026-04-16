namespace Application.OrdensServico.Services;

using Domain.OrdensServico.Interfaces;
using Application.OrdensServico.DTOs;
using Domain.OrdensServico.Entities;
using Domain.Estoque.Interfaces;
using Application.Estoque.DTOs;
using Domain.Estoque.Entities;
using Application.Estoque.Interfaces;
using Application.Notificacao.Interfaces;

public class OrdemServicoService
{
    private readonly IOrdemServicoRepository _repo;
    private readonly IServicoRepository _servicoRepo;
    private readonly IPecaRepository _pecaRepo;
    private readonly IEstoqueService _estoqueService;
    private readonly INotificacaoService _notificacaoService;

    public OrdemServicoService(
        IOrdemServicoRepository repo,
        IServicoRepository servicoRepo,
        IPecaRepository pecaRepo,
			  IEstoqueService estoqueService,
				INotificacaoService notificacaoService)
    {
        _repo = repo;
        _servicoRepo = servicoRepo;
        _pecaRepo = pecaRepo;
        _estoqueService = estoqueService;
        _notificacaoService = notificacaoService;
    }

    public async Task<IEnumerable<OrdemServicoResponseDto>> GetAll()
	  {
        var ordemServicos = await _repo.ObterTodos();

				foreach(var ordemServico in ordemServicos)
				{
					foreach(var servico in ordemServico.OrdemServicoServicos)
					{
                Console.WriteLine($"servico: {servico.Id} FK: {servico.ServicoId}");
            }
				}

        return ordemServicos.Select(os => new OrdemServicoResponseDto
        {
            Id = os.Id,
            ClienteId = os.ClienteId,
						VeiculoId = os.VeiculoId,
						Total = os.Total,
						Status = os.Status,
						Servicos = os.OrdemServicoServicos?.Select(s => new OrdemServicoServicoDto
						{
							ServicoId = s.ServicoId,
							Preco = s.Preco,
							Nome = s.Servico?.Nome
						}).ToList() ?? new List<OrdemServicoServicoDto>(),
						
						Pecas = os.OrdemServicoPecas?.Select(p => new OrdemServicoPecaDto
						{
							PecaId = p.PecaId,
							Preco = p.Preco,
							Quantidade = p.Quantidade,
							Nome = p.Peca.Nome
						}).ToList() ?? new List<OrdemServicoPecaDto>()
				});
		}

		public async Task<OrdemServicoResponseDto> ObterPorId(Guid id)
	  {
        var os = await _repo.ObterPorId(id);
        var dto = new OrdemServicoResponseDto
        {
            ClienteId = os.ClienteId,
            VeiculoId = os.VeiculoId,
            Total = os.Total,
            Status = os.Status,
            Servicos = os.OrdemServicoServicos?.Select(s => new OrdemServicoServicoDto
            {
                ServicoId = s.ServicoId,
                Preco = s.Preco,
                Nome = s.Servico?.Nome
            }).ToList() ?? new List<OrdemServicoServicoDto>(),

            Pecas = os.OrdemServicoPecas?.Select(p => new OrdemServicoPecaDto
            {
                PecaId = p.PecaId,
                Preco = p.Preco,
                Quantidade = p.Quantidade,
                Nome = p.Peca.Nome
            }).ToList() ?? new List<OrdemServicoPecaDto>()
        };

        return dto;
    }

		public async Task Criar(OrdemServicoCreateDto dto)
	  {
        var ordemServico = new OrdemServico(dto.ClienteId, dto.VeiculoId);

        ordemServico.Status = StatusOrdemServico.Recebida;

        await _repo.AdicionarPecas(ordemServico);
    }

		public async Task Deletar(OrdemServicoDeleteDto dto)
	{
        await _repo.Deletar(dto.Id);
    }

		public async Task AdicionarPecas(OrdemServicoAdicionaPecaDto dto)
	  {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);
        foreach (var peca in dto.Pecas){
					ordemServico.AdicionarPeca(peca.PecaId, peca.Preco, peca.Quantidade);
        }

        await _repo.AdicionarPecas(ordemServico);
    }

	  public async Task AdicionarServicos(OrdemServicoAdicionaServicoDto dto)
	  {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);

				foreach (var servico in dto.Servicos)
				{
            ordemServico.AdicionarServico(servico.ServicoId, servico.Preco);
        }

        await _repo.SaveChangesAsync();
    }

	  public async Task EnviarOrcamento(OrdemServicoEnviaOrcamentoDto dto)
	  {
        await _notificacaoService.EnviarOrcamento(dto.OrdemServicoId);
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

				foreach (var peca in ordemServico.OrdemServicoPecas)
				{
            await _estoqueService.Consumir(peca.Id, peca.Quantidade);
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

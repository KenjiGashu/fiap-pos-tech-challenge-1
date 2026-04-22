namespace Application.OrdensServico.Services;

using Domain.OrdensServico.Interfaces;
using Application.OrdensServico.DTOs;
using Application.Notificacao.DTOs;
using Domain.OrdensServico.Entities;
using Application.Estoque.Interfaces;
using Application.Notificacao.Interfaces;
using Application.OrdensServico.Interfaces;

public class OrdemServicoService : IOrdemServicoService
{
    private readonly IOrdemServicoRepository _repo;
    private readonly IEstoqueService _estoqueService;
    private readonly INotificacaoService _notificacaoService;
    private readonly ITokenService _tokenService;
    private readonly IClienteRepository _clienteRepo;

    public OrdemServicoService(
        IOrdemServicoRepository repo,
        IEstoqueService estoqueService,
        INotificacaoService notificacaoService,
        ITokenService tokenService,
        IClienteRepository clienteRepo)
    {
        _repo = repo;
        _estoqueService = estoqueService;
        _notificacaoService = notificacaoService;
        _tokenService = tokenService;
        _clienteRepo = clienteRepo;
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

        await _repo.Criar(ordemServico);
    }

    public async Task Deletar(OrdemServicoDeleteDto dto)
    {
        await _repo.Deletar(dto.Id);
    }

    public async Task AdicionarPecas(OrdemServicoAdicionaPecaDto dto)
    {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);
        foreach (var pecaDto in dto.Pecas){
            ordemServico.AdicionarPeca(pecaDto.PecaId, pecaDto.Preco, pecaDto.Quantidade, pecaDto.Nome);
        }

        await _repo.SaveChangesAsync();
    }

    public async Task AdicionarServicos(OrdemServicoAdicionaServicoDto dto)
    {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);

        foreach (var servico in dto.Servicos)
        {
            ordemServico.AdicionarServico(servico.ServicoId, servico.Preco, servico.Nome);
        }

        await _repo.SaveChangesAsync();
    }

    public async Task EnviarOrcamento(OrdemServicoEnviarOrcamentoDto dto)
    {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);
        var cliente = await _clienteRepo.ObterPorId(ordemServico.ClienteId);
        var token = await _tokenService.GeraToken(ordemServico.Id);

        var aprovacaoOrcamentoDto = new AprovacaoOrcamentoDto
        {
            OrdemServicoId = dto.OrdemServicoId,
            Servicos = ordemServico.OrdemServicoServicos.Select(oss => new ItemOrcamentoDto(oss.Nome, oss.Preco, 0)),
            Pecas = ordemServico.OrdemServicoPecas.Select(osp => new ItemOrcamentoDto(osp.Nome, osp.Preco, osp.Quantidade)),
            Total = ordemServico.Total,
            NomeCliente = cliente.Nome,
            Destinatario = cliente.GetDestinatario(),
            TokenGuid = token.GuidToken,
        };

        await _notificacaoService.EnviarOrcamento(aprovacaoOrcamentoDto);
    }

    public async Task AprovarOrcamento(OrdemServicoAprovarOrcamentoDto dto)
    {
        var token = await _tokenService.ObterTokenPorGuid(dto.TokenGuid);

        if(!token.IsValid())
            throw new Exception("Token Expirado ou ja consumido");

        token.ConsumirToken();

        var os = await _repo.ObterPorId(token.OrdemServicoId);
        os.AprovarOrcamento();

        await _repo.SaveChangesAsync();
    }

    public async Task IniciarDiagnostico(OrdemServicoIniciarDiagnosticoOrcamentoDto dto)
    {
        var os = await _repo.ObterPorId(dto.OrdemServicoId);
        os.IniciarDiagnostico();

        await _repo.SaveChangesAsync();
    }

    public async Task FinalizarDiagnostico(OrdemServicoFinalizarDiagnosticoOrcamentoDto dto)
    {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);

        ordemServico.FinalizarDiagnostico();

        if(ordemServico.deveAprovarDeNovo)
        {
            await EnviarOrcamento(new OrdemServicoEnviarOrcamentoDto
            {
                OrdemServicoId = dto.OrdemServicoId
            });
        }

        await _repo.SaveChangesAsync();
    }
    
    public async Task IniciarExecucao(OrdemServicoIniciarExecucaoOrcamentoDto dto)
    {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);

        foreach (var peca in ordemServico.OrdemServicoPecas)
        {
            await _estoqueService.Consumir(peca.PecaId, peca.Quantidade);
        }
                
        ordemServico.IniciarExecucao();

        await _repo.SaveChangesAsync();
    }

    public async Task FinalizarExecucao(OrdemServicoFinalizarExecucaoOrcamentoDto dto)
    {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);

        ordemServico.FinalizarExecucao();

        await _repo.SaveChangesAsync();
    }

    public async Task EntregarVeiculo(OrdemServicoEntregarVeiculoDto dto)
    {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);

        ordemServico.EntregarVeiculo();

        await _repo.SaveChangesAsync();
    }
}

namespace Gashu.SistemaMecanica.Application.OrdensServico.Services;

using Gashu.SistemaMecanica.Domain.OrdensServico.Interfaces;
using Gashu.SistemaMecanica.Application.OrdensServico.DTOs;
using Gashu.SistemaMecanica.Application.Notificacao.DTOs;
using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;
using Gashu.SistemaMecanica.Application.Estoque.Interfaces;
using Gashu.SistemaMecanica.Application.Notificacao.Interfaces;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;
using Gashu.SistemaMecanica.Application.Metricas.Interfaces;
using Gashu.SistemaMecanica.Application.Metricas.DTOs;

public class OrdemServicoService : IOrdemServicoService
{
    private readonly IOrdemServicoRepository _repo;
    private readonly IEstoqueService _estoqueService;
    private readonly INotificacaoService _notificacaoService;
    private readonly ITokenService _tokenService;
    private readonly IMetricaOrdemServicoService _metricaService;
    private readonly IVeiculoService _veiculoService;
    private readonly IClienteService _clienteService;
    private readonly IClienteRepository _clienteRepo;

    public OrdemServicoService(
        IOrdemServicoRepository repo,
        IEstoqueService estoqueService,
        INotificacaoService notificacaoService,
        ITokenService tokenService,
        IMetricaOrdemServicoService metricaService,
        IVeiculoService veiculoService,
        IClienteService clienteService,
        IClienteRepository clienteRepository)
    {
        _repo = repo;
        _estoqueService = estoqueService;
        _notificacaoService = notificacaoService;
        _tokenService = tokenService;
        _metricaService = metricaService;
        _veiculoService = veiculoService;
        _clienteService = clienteService;
        _clienteRepo = clienteRepository;
    }

    public async Task<IEnumerable<OrdemServicoResponseDto>> GetAll()
    {
        var ordemServicos = await _repo.ObterTodos();
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

    public async Task<IEnumerable<OrdemServicoResponseDto>> ListaOrdensServicos()
    {
        var ordemServicos = await _repo.ObterTodos();
        
        return ordemServicos
            .OrderBy(os => os.Status switch
            {
                StatusOrdemServico.EmExecucao => 1,
                StatusOrdemServico.AguardandoAprovacao => 2,
                StatusOrdemServico.EmDiagnostico => 3,
                StatusOrdemServico.Recebida => 4,
                StatusOrdemServico.Entregue => 5,
                StatusOrdemServico.Finalizada => 6,
                StatusOrdemServico.AguardandoMecanico => 7,
                StatusOrdemServico.AguardandoAprovacaoRevisao => 8,
                StatusOrdemServico.OrcamentoAprovado => 9,
                StatusOrdemServico.OrcamentoRejeitado => 10,
            })
            .ThenBy(os => os.Data)
            .Where(os => os.Status != StatusOrdemServico.Entregue && os.Status != StatusOrdemServico.Finalizada)
            .Select(os => new OrdemServicoResponseDto
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
        };

        return dto;
    }

    public async Task<ListaOrdemServicoResponseDto> ObterPorIdCliente(Guid clienteId)
    {
        Console.WriteLine($"[obter por id cliente] {clienteId}");
        var ordemServicos = await _repo.ObterPorIdCliente(clienteId);
        var dto = new ListaOrdemServicoResponseDto
        {
            OrdemServicos = ordemServicos.Select(os => new OrdemServicoResponseDto
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

            }).ToList()
        };

        return dto;
    }

    public async Task Criar(OrdemServicoCreateDto dto)
    {
        var ordemServico = new OrdemServico(dto.ClienteId, dto.VeiculoId);

        ordemServico.Status = StatusOrdemServico.Recebida;

        await _repo.Criar(ordemServico);

        // atualiza metricas
        SalvarMetricaOrdemServicoDto metricaDto = new SalvarMetricaOrdemServicoDto()
        {
            OrdemServicoId = ordemServico.Id,
            Status = ordemServico.Status.ToString()
        };
        await _metricaService.SalvaMetricaOrdemServico(metricaDto);
    }

    public async Task CriarComTodosOsDados(OrdemServicoCreateDtoTodosDados dto)
    {
        // criar cliente quando ele nao existe
        ClienteResponseDto clienteDto;
        if (dto.Cliente.TipoPessoa == TipoPessoa.Fisica)
            clienteDto = await _clienteService.GetByCpf(dto.Cliente.Cpf);
        else
            clienteDto = await _clienteService.GetByCnpj(dto.Cliente.Cnpj);

        if (clienteDto == null)
        {
            ClienteCreateDto createDto = new ClienteCreateDto
            {
                Nome = dto.Cliente.Nome,
                Cpf = dto.Cliente.Cpf,
                Cnpj = dto.Cliente.Cnpj,
                TipoPessoa = dto.Cliente.TipoPessoa
            };
            await _clienteService.Create(createDto);
        }

        VeiculoResponseDto veiculoDto;
        // inserir ou obter veiculo
        try {
             veiculoDto = await _veiculoService.GetByPlaca(dto.Veiculo.Placa);
        } catch (Exception ex) {
            //veiculo nao encontrado
            VeiculoCreateDto veiculoCreateDto = new VeiculoCreateDto
            {
                Ano = dto.Veiculo.Ano,
                Marca = dto.Veiculo.Marca,
                Modelo = dto.Veiculo.Modelo,
                Placa = dto.Veiculo.Placa
            };
            await _veiculoService.Create(veiculoCreateDto);
        }

        // pegar id do cliente e id do veiculo
        if (dto.Cliente.TipoPessoa == TipoPessoa.Fisica)
            clienteDto = await _clienteService.GetByCpf(dto.Cliente.Cpf);
        else
            clienteDto = await _clienteService.GetByCnpj(dto.Cliente.Cnpj);
        veiculoDto = await _veiculoService.GetByPlaca(dto.Veiculo.Placa);

        // criar ordem servico
        var ordemServico = new OrdemServico(clienteDto.Id, veiculoDto.Id);
        ordemServico.Status = StatusOrdemServico.Recebida;
        await _repo.Criar(ordemServico);

        // adicionar pecas e servicos
        foreach (var servico in dto.Servicos)
        {
            ordemServico.AdicionarServico(servico.ServicoId, servico.Preco, servico.Nome);
        }
        foreach (var peca in dto.Pecas)
        {
            ordemServico.AdicionarPeca(peca.PecaId, peca.Preco, peca.Quantidade, peca.Nome);
        }

        await _repo.SaveChangesAsync();

        // atualiza metricas
        SalvarMetricaOrdemServicoDto metricaDto = new SalvarMetricaOrdemServicoDto()
        {
            OrdemServicoId = ordemServico.Id,
            Status = ordemServico.Status.ToString()
        };
        await _metricaService.SalvaMetricaOrdemServico(metricaDto);
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

        if(cliente == null || token == null)
        {
            throw new Exception("Dados invalidos para envio de orcamento");
        }

        ordemServico.EnviarOrcamento();

        // atualiza metricas
        SalvarMetricaOrdemServicoDto metricaDto = new SalvarMetricaOrdemServicoDto()
        {
            OrdemServicoId = dto.OrdemServicoId,
            Status = ordemServico.Status.ToString()
        };

        var aprovacaoOrcamentoDto = new AprovacaoOrcamentoDto
        {
            OrdemServicoId = dto.OrdemServicoId,
            Servicos = ordemServico.OrdemServicoServicos.Select(oss => new ItemOrcamentoDto
            {
                Nome = oss.Nome,
                Preco = oss.Preco,
                Quantidade = 0
            }),
            Pecas = ordemServico.OrdemServicoPecas.Select(osp => new ItemOrcamentoDto
            {
              Nome = osp.Nome,
              Preco = osp.Preco,
              Quantidade = osp.Quantidade
            }),

            Total = ordemServico.Total,
            NomeCliente = cliente.Nome,
            Destinatario = cliente.GetDestinatario(),
            TokenGuid = token.GuidToken,
        };

        await _notificacaoService.EnviarOrcamento(aprovacaoOrcamentoDto);

        await _metricaService.SalvaMetricaOrdemServico(metricaDto);
    }

    public async Task AprovarOrcamento(OrdemServicoAprovarOrcamentoDto dto)
    {
        // token
        var token = await _tokenService.ObterTokenPorGuid(dto.TokenGuid);
        if(token == null || !token.IsValid())
            throw new Exception("Token Expirado ou ja consumido");
        token.ConsumirToken();

        var os = await _repo.ObterPorId(token.OrdemServicoId);
        os.AprovarOrcamento();

        // atualiza metricas
        SalvarMetricaOrdemServicoDto metricaDto = new SalvarMetricaOrdemServicoDto()
        {
            OrdemServicoId = os.Id,
            Status = os.Status.ToString()
        };
        await _metricaService.SalvaMetricaOrdemServico(metricaDto);

        await _repo.SaveChangesAsync();
    }

    public async Task RejeitarOrcamento(OrdemServicoRejeitarOrcamentoDto dto)
    {
        // token
        var token = await _tokenService.ObterTokenPorGuid(dto.TokenGuid);
        if(token == null || !token.IsValid())
            throw new Exception("Token Expirado ou ja consumido");
        token.ConsumirToken();

        var os = await _repo.ObterPorId(token.OrdemServicoId);
        os.RejeitarOrcamento();

        // atualiza metricas
        SalvarMetricaOrdemServicoDto metricaDto = new SalvarMetricaOrdemServicoDto()
        {
            OrdemServicoId = os.Id,
            Status = os.Status.ToString()
        };
        await _metricaService.SalvaMetricaOrdemServico(metricaDto);

        await _repo.SaveChangesAsync();
    }

    public async Task IniciarDiagnostico(OrdemServicoIniciarDiagnosticoOrcamentoDto dto)
    {
        var os = await _repo.ObterPorId(dto.OrdemServicoId);
        var cliente = await _clienteRepo.ObterPorId(os.ClienteId);

        if(cliente == null)
            throw new Exception("Cliente nao encontrado, nao podemos prosseguir com inicializacao de diagnostico");

        os.IniciarDiagnostico();

        // atualiza metricas
        SalvarMetricaOrdemServicoDto metricaDto = new SalvarMetricaOrdemServicoDto()
        {
            OrdemServicoId = os.Id,
            Status = os.Status.ToString()
        };
        await _metricaService.SalvaMetricaOrdemServico(metricaDto);

        var mensagem = $"Status da ordem de servico atualizada para {StatusOrdemServico.EmDiagnostico.ToString()}";
        var titulo = $"Atualizacao Status Ordem Servico {os.Id}";
        await _notificacaoService.EnviarMensagem(cliente.GetDestinatario(), titulo, mensagem);

        await _repo.SaveChangesAsync();
    }

    public async Task FinalizarDiagnostico(OrdemServicoFinalizarDiagnosticoOrcamentoDto dto)
    {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);
        var cliente = await _clienteRepo.ObterPorId(ordemServico.ClienteId);

        if(cliente == null)
            throw new Exception("Cliente nao encontrado, nao podemos prosseguir com finalizacao de diagnostico");

        ordemServico.FinalizarDiagnostico();

        if(ordemServico.deveAprovarDeNovo)
        {
            await EnviarOrcamento(new OrdemServicoEnviarOrcamentoDto
            {
                OrdemServicoId = dto.OrdemServicoId
            });
        }

        // atualiza metricas
        SalvarMetricaOrdemServicoDto metricaDto = new SalvarMetricaOrdemServicoDto()
        {
            OrdemServicoId = ordemServico.Id,
            Status = ordemServico.Status.ToString()
        };
        await _metricaService.SalvaMetricaOrdemServico(metricaDto);

        var mensagem = $"Status da ordem de servico atualizada para {StatusOrdemServico.AguardandoMecanico.ToString()}";
        var titulo = $"Atualizacao Status Ordem Servico {ordemServico.Id}";
        await _notificacaoService.EnviarMensagem(cliente.GetDestinatario(), titulo, mensagem);
        
        await _repo.SaveChangesAsync();
    }
    
    public async Task IniciarExecucao(OrdemServicoIniciarExecucaoOrcamentoDto dto)
    {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);
        var cliente = await _clienteRepo.ObterPorId(ordemServico.ClienteId);

        foreach (var peca in ordemServico.OrdemServicoPecas)
        {
            await _estoqueService.Consumir(peca.PecaId, peca.Quantidade);
        }
                
        ordemServico.IniciarExecucao();

        // atualiza metricas
        SalvarMetricaOrdemServicoDto metricaDto = new SalvarMetricaOrdemServicoDto()
        {
            OrdemServicoId = ordemServico.Id,
            Status = ordemServico.Status.ToString()
        };
        await _metricaService.SalvaMetricaOrdemServico(metricaDto);

        var mensagem = $"Status da ordem de servico atualizada para {StatusOrdemServico.EmExecucao.ToString()}";
        var titulo = $"Atualizacao Status Ordem Servico {ordemServico.Id}";
        await _notificacaoService.EnviarMensagem(cliente.GetDestinatario(), titulo, mensagem);

        await _repo.SaveChangesAsync();
    }

    public async Task FinalizarExecucao(OrdemServicoFinalizarExecucaoOrcamentoDto dto)
    {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);
        var cliente = await _clienteRepo.ObterPorId(ordemServico.ClienteId);

        ordemServico.FinalizarExecucao();

        // atualiza metricas
        SalvarMetricaOrdemServicoDto metricaDto = new SalvarMetricaOrdemServicoDto()
        {
            OrdemServicoId = ordemServico.Id,
            Status = ordemServico.Status.ToString()
        };
        await _metricaService.SalvaMetricaOrdemServico(metricaDto);
       
        var mensagem = $"Status da ordem de servico atualizada para {StatusOrdemServico.Finalizada.ToString()}";
        var titulo = $"Atualizacao Status Ordem Servico {ordemServico.Id}";
        await _notificacaoService.EnviarMensagem(cliente.GetDestinatario(), titulo, mensagem);

        await _repo.SaveChangesAsync();
    }

    public async Task EntregarVeiculo(OrdemServicoEntregarVeiculoDto dto)
    {
        var ordemServico = await _repo.ObterPorId(dto.OrdemServicoId);
        var cliente = await _clienteRepo.ObterPorId(ordemServico.ClienteId);

        ordemServico.EntregarVeiculo();

        // atualiza metricas
        SalvarMetricaOrdemServicoDto metricaDto = new SalvarMetricaOrdemServicoDto()
        {
            OrdemServicoId = ordemServico.Id,
            Status = ordemServico.Status.ToString()
        };
        await _metricaService.SalvaMetricaOrdemServico(metricaDto);
        
        var mensagem = $"Status da ordem de servico atualizada para {StatusOrdemServico.Entregue.ToString()}";
        var titulo = $"Atualizacao Status Ordem Servico {ordemServico.Id}";
        await _notificacaoService.EnviarMensagem(cliente.GetDestinatario(), titulo, mensagem);

        await _repo.SaveChangesAsync();
    }
}

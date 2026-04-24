namespace Tests.Application.OrdensServico;

using Moq;
using global::Application.OrdensServico.DTOs;
using global::Application.OrdensServico.Services;
using global::Domain.OrdensServico.Interfaces;
using global::Domain.OrdensServico.Entities;
using global::Application.OrdensServico.Interfaces;
using global::Application.Estoque.Interfaces;
using global::Application.Notificacao.Interfaces;
using global::Application.Notificacao.DTOs;
using global::Domain.Identidade.Entities;

public class OrdemServicoServiceTests
{
    Guid clienteId;
    Guid veiculoId;
    Mock<IOrdemServicoRepository> _mockOsRepo;
    Mock<IEstoqueService> _mockEstoqueService;
    Mock<INotificacaoService> _mockNotificacaoService;
    Mock<ITokenService> _mockTokenService;
    Mock<IClienteRepository> _mockClienteRepo;
    IOrdemServicoService service;

    public OrdemServicoServiceTests()
    {
        clienteId = Guid.NewGuid();
        veiculoId = Guid.NewGuid();
        _mockOsRepo = new Mock<IOrdemServicoRepository>();
        _mockEstoqueService = new Mock<IEstoqueService>();
        _mockNotificacaoService = new Mock<INotificacaoService>();
        _mockTokenService = new Mock<ITokenService>();
        _mockClienteRepo = new Mock<IClienteRepository>();
        service = new OrdemServicoService(_mockOsRepo.Object, _mockEstoqueService.Object,
             _mockNotificacaoService.Object, _mockTokenService.Object, _mockClienteRepo.Object);
    }

    [Fact]
    public async Task CriaOrdemServicoTest()
    {
        //Arrange
        var ordemServicoDto = new OrdemServicoCreateDto
        {
            ClienteId = clienteId,
            VeiculoId = veiculoId
        };

        //Act
        await service.Criar(ordemServicoDto);

        //Assert
        _mockOsRepo.Setup(repo => repo.Criar(It.Is<OrdemServico>(os => os.Status == StatusOrdemServico.Recebida)));

        _mockOsRepo.Verify(repo => repo.Criar(It.Is<OrdemServico>(os => os.Status == StatusOrdemServico.Recebida)));
    }

    [Fact]
    public async Task OrdemServicoAdicionarPecaTest()
    {
        //Arrange
        var ordemServico = new OrdemServico(Guid.NewGuid(), Guid.NewGuid());
        ordemServico.Id = Guid.NewGuid();

        _mockOsRepo.Setup(repo =>
                        repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)))
            .ReturnsAsync(ordemServico);

        var pecaGuid = Guid.NewGuid();
        var ordemServicoId = ordemServico.Id;
        var ordemServicoPecaDto = new OrdemServicoPecaDto
        {
            PecaId = pecaGuid,
            Nome = "pneu",
            Preco = 10,
            Quantidade = 1
        };

        var pecas = new List<OrdemServicoPecaDto>();
        pecas.Add(ordemServicoPecaDto);

        var ordemServicoAdicionaPecaDto = new OrdemServicoAdicionaPecaDto
        {
            OrdemServicoId = ordemServicoId,
            Pecas = pecas
        };


        //act
        await service.AdicionarPecas(ordemServicoAdicionaPecaDto);

        //Assert
        _mockOsRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServicoId)));
        var peca = ordemServico.OrdemServicoPecas.First();
        Assert.Equal("pneu", peca.Nome);
        Assert.Equal(10, peca.Preco);
        _mockOsRepo.Verify(repo => repo.SaveChangesAsync());
    }

    [Fact]
    public async Task OrdemServicoAdicionarServicoTest()
    {
        //Arrange
        var ordemServico = new OrdemServico(Guid.NewGuid(), Guid.NewGuid());
        ordemServico.Id = Guid.NewGuid();

        _mockOsRepo.Setup(repo =>
                        repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)))
            .ReturnsAsync(ordemServico);

        var servicoGuid = Guid.NewGuid();
        var ordemServicoId = ordemServico.Id;
        var ordemServicoServicoDto = new OrdemServicoServicoDto
        {
            ServicoId = servicoGuid,
            Nome = "alinhamento",
            Preco = 10,
        };

        var servicos = new List<OrdemServicoServicoDto>();
        servicos.Add(ordemServicoServicoDto);

        var ordemServicoAdicionaServicoDto = new OrdemServicoAdicionaServicoDto
        {
            OrdemServicoId = ordemServicoId,
            Servicos = servicos
        };


        //act
        await service.AdicionarServicos(ordemServicoAdicionaServicoDto);

        //Assert
        _mockOsRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServicoId)));
        var servico = ordemServico.OrdemServicoServicos.First();
        Assert.Equal("alinhamento", servico.Nome);
        Assert.Equal(10, servico.Preco);
        _mockOsRepo.Verify(repo => repo.SaveChangesAsync());
    }

    [Fact]
    public async Task OrdemServicoEnviarOrcamentoTest()
    {
        //Arrange
        //ordem servico
        var ordemServico = new OrdemServico(Guid.NewGuid(), Guid.NewGuid());
        ordemServico.Id = Guid.NewGuid();

        _mockOsRepo.Setup(repo =>
                        repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)))
            .ReturnsAsync(ordemServico);

        //ordem servico peca
        var pecaId = Guid.NewGuid();
        var nomePeca = "pneu";
        var quantidadePeca = 1;
        var precoPeca = 12;

        //ordem servico peca
        var servicoId = Guid.NewGuid();
        var nomeServico = "pneu";
        var precoServico = 12;

        ordemServico.AdicionarPeca(pecaId, precoPeca, quantidadePeca, nomePeca);
        ordemServico.AdicionarServico(servicoId, precoServico, nomeServico);

        //cliente
        var nomeCliente = "maria";
        var emailCliente = "maria@gmail.com";
        var usuario = new Usuario(emailCliente, "1234");
        var cliente = new Cliente(nomeCliente, "433.023.538-20", "", TipoPessoa.Fisica);
        cliente.Usuario = usuario;
        cliente.Id = ordemServico.ClienteId;

        _mockClienteRepo.Setup(repo =>
                               repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.ClienteId)))
            .ReturnsAsync(cliente);

        //token
        var guidString = Guid.NewGuid().ToString("n");
        var token = new Token(guidString, DateTime.Now.AddDays(1), ordemServico.Id);

        _mockTokenService.Setup(repo =>
                                repo.GeraToken(It.Is<Guid>(id => id == ordemServico.Id)))
            .ReturnsAsync(token);

        //notificacao
        _mockNotificacaoService.Setup(service => service.EnviarOrcamento(It.IsAny<AprovacaoOrcamentoDto>()));

        var ordemServicoEnviarOrcamentoDto = new OrdemServicoEnviarOrcamentoDto
        {
            OrdemServicoId = ordemServico.Id
        };

        // para validar o dto enviado para notificacao servico
        Func<AprovacaoOrcamentoDto, bool> validateDto = dto =>
        {
            var result = dto.OrdemServicoId == ordemServico.Id &&
               dto.Total == ordemServico.Total &&
                dto.NomeCliente == nomeCliente &&
                dto.Destinatario == emailCliente &&
                dto.TokenGuid == token.GuidToken;

            return result;
        };

        //Act
        await service.EnviarOrcamento(ordemServicoEnviarOrcamentoDto);

        //Assert
        _mockOsRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)));
        _mockClienteRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.ClienteId)));
        _mockTokenService.Verify(repo => repo.GeraToken(It.Is<Guid>(id => id == ordemServico.Id)));
        _mockNotificacaoService.Verify(service => service.EnviarOrcamento(It.Is<AprovacaoOrcamentoDto>(dto => validateDto(dto))));
    }

    [Theory]
    [InlineData(StatusOrdemServico.AguardandoAprovacao, StatusOrdemServico.OrcamentoAprovado)]
    [InlineData(StatusOrdemServico.AguardandoAprovacaoRevisao, StatusOrdemServico.AguardandoMecanico)]
    public async Task OrdemServicoAprovarOrcamentoTest(StatusOrdemServico from, StatusOrdemServico to)
    {
        //Arrange
        //ordem servico
        var ordemServico = new OrdemServico(Guid.NewGuid(), Guid.NewGuid());
        ordemServico.Id = Guid.NewGuid();
        ordemServico.Status = from;

        _mockOsRepo.Setup(repo =>
                        repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)))
            .ReturnsAsync(ordemServico);

        //token
        var guidString = Guid.NewGuid().ToString("n");
        var token = new Token(guidString, DateTime.Now.AddDays(1), ordemServico.Id);
        token.Consumido = false;

        _mockTokenService.Setup(repo =>
                                repo.ObterTokenPorGuid(It.IsAny<Guid>()))
            .ReturnsAsync(token);

        var ordemServicoAprovarOrcamentoDto = new OrdemServicoAprovarOrcamentoDto
        {
            TokenGuid = new Guid(token.GuidToken)
        };

        //Act
        await service.AprovarOrcamento(ordemServicoAprovarOrcamentoDto);

        //Assert
        _mockOsRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)));
        _mockTokenService.Verify(repo => repo.ObterTokenPorGuid(It.IsAny<Guid>()));
        Assert.Equal(to, ordemServico.Status);
        Assert.True(token.Consumido);
    }

    [Fact]
    public async Task OrdemServicoIniciarDiagnosticoTest()
    {
        //Arrange
        //ordem servico
        var ordemServico = new OrdemServico(Guid.NewGuid(), Guid.NewGuid());
        ordemServico.Id = Guid.NewGuid();

        _mockOsRepo.Setup(repo =>
                        repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)))
            .ReturnsAsync(ordemServico);

        var ordemServicoIniciarDiagnosticoOrcamentoDto = new OrdemServicoIniciarDiagnosticoOrcamentoDto
        {
            OrdemServicoId = ordemServico.Id
        };

        //Act
        await service.IniciarDiagnostico(ordemServicoIniciarDiagnosticoOrcamentoDto);

        //Assert
        _mockOsRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)));
        Assert.Equal(StatusOrdemServico.EmDiagnostico, ordemServico.Status);
    }

    [Fact]
    public async Task OrdemServicoFinalizarDiagnosticoTest()
    {
        //Arrange
        //ordem servico
        var ordemServico = new OrdemServico(Guid.NewGuid(), Guid.NewGuid());
        ordemServico.Id = Guid.NewGuid();
        ordemServico.deveAprovarDeNovo = false;

        _mockOsRepo.Setup(repo =>
                        repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)))
            .ReturnsAsync(ordemServico);

        var ordemServicoFinalizarDiagnosticoOrcamentoDto = new OrdemServicoFinalizarDiagnosticoOrcamentoDto
        {
            OrdemServicoId = ordemServico.Id
        };

        //Act
        await service.FinalizarDiagnostico(ordemServicoFinalizarDiagnosticoOrcamentoDto);

        //Assert
        _mockOsRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)));
        Assert.Equal(StatusOrdemServico.AguardandoMecanico, ordemServico.Status);
    }

    [Fact]
    public async Task OrdemServicoEnviarOrcamentoDeveAprovarDeNovoTest()
    {
        //Arrange
        //ordem servico
        var ordemServico = new OrdemServico(Guid.NewGuid(), Guid.NewGuid());
        ordemServico.Id = Guid.NewGuid();
        ordemServico.deveAprovarDeNovo = true;

        _mockOsRepo.Setup(repo =>
                        repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)))
            .ReturnsAsync(ordemServico);

        //ordem servico peca
        var pecaId = Guid.NewGuid();
        var nomePeca = "pneu";
        var quantidadePeca = 1;
        var precoPeca = 12;

        //ordem servico peca
        var servicoId = Guid.NewGuid();
        var nomeServico = "pneu";
        var precoServico = 12;

        ordemServico.AdicionarPeca(pecaId, precoPeca, quantidadePeca, nomePeca);
        ordemServico.AdicionarServico(servicoId, precoServico, nomeServico);

        //cliente
        var nomeCliente = "maria";
        var emailCliente = "maria@gmail.com";
        var usuario = new Usuario(emailCliente, "1234");
        var cliente = new Cliente(nomeCliente, "433.023.538-20", "", TipoPessoa.Fisica);
        cliente.Usuario = usuario;
        cliente.Id = ordemServico.ClienteId;

        _mockClienteRepo.Setup(repo =>
                               repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.ClienteId)))
            .ReturnsAsync(cliente);

        //token
        var guidString = Guid.NewGuid().ToString("n");
        var token = new Token(guidString, DateTime.Now.AddDays(1), ordemServico.Id);

        _mockTokenService.Setup(repo =>
                                repo.GeraToken(It.Is<Guid>(id => id == ordemServico.Id)))
            .ReturnsAsync(token);

        //notificacao
        _mockNotificacaoService.Setup(service => service.EnviarOrcamento(It.IsAny<AprovacaoOrcamentoDto>()));

        var ordemServicoFinalizarDiagnosticoOrcamentoDto = new OrdemServicoFinalizarDiagnosticoOrcamentoDto
        {
            OrdemServicoId = ordemServico.Id
        };

        // para validar o dto enviado para notificacao servico
        Func<AprovacaoOrcamentoDto, bool> validateDto = dto =>
        {
            var result = dto.OrdemServicoId == ordemServico.Id &&
               dto.Total == ordemServico.Total &&
                dto.NomeCliente == nomeCliente &&
                dto.Destinatario == emailCliente &&
                dto.TokenGuid == token.GuidToken;

            return result;
        };

        //Act
        await service.FinalizarDiagnostico(ordemServicoFinalizarDiagnosticoOrcamentoDto);

        //Assert
        _mockOsRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)));
        _mockClienteRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.ClienteId)));
        _mockTokenService.Verify(repo => repo.GeraToken(It.Is<Guid>(id => id == ordemServico.Id)));
        _mockNotificacaoService.Verify(service => service.EnviarOrcamento(It.Is<AprovacaoOrcamentoDto>(dto => validateDto(dto))));
    }

    [Fact]
    public async Task OrdemServicoIniciarExecucaoTest()
    {
        //Arrange
        //ordem servico
        var ordemServico = new OrdemServico(Guid.NewGuid(), Guid.NewGuid());
        ordemServico.Id = Guid.NewGuid();

        _mockOsRepo.Setup(repo =>
                        repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)))
            .ReturnsAsync(ordemServico);

        //estoque
        var pecaId = Guid.NewGuid();
        var nomePeca = "pneu";
        var quantidadePeca = 1;
        var precoPeca = 12;
        ordemServico.AdicionarPeca(pecaId, precoPeca, quantidadePeca, nomePeca);

        var peca2Id = Guid.NewGuid();
        var nomePeca2 = "Motor";
        var quantidadePeca2 = 1;
        var precoPeca2 = 120;
        ordemServico.AdicionarPeca(peca2Id, precoPeca2, quantidadePeca2, nomePeca2);

        _mockEstoqueService.Setup(service =>
            service.Consumir(pecaId, quantidadePeca));
        
        _mockEstoqueService.Setup(service =>
            service.Consumir(peca2Id, quantidadePeca2));
        
        var ordemServicoIniciarExecucaoOrcamentoDto = new OrdemServicoIniciarExecucaoOrcamentoDto
        {
            OrdemServicoId = ordemServico.Id
        };

        //Act
        await service.IniciarExecucao(ordemServicoIniciarExecucaoOrcamentoDto);

        //Assert
        _mockOsRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)));
        _mockEstoqueService.Verify(service => service.Consumir(pecaId, quantidadePeca));
        _mockEstoqueService.Verify(service => service.Consumir(peca2Id, quantidadePeca2));
        Assert.Equal(StatusOrdemServico.EmExecucao, ordemServico.Status);
    }

    [Fact]
    public async Task OrdemServicoFinalizarExecucaoTest()
    {
        //Arrange
        //ordem servico
        var ordemServico = new OrdemServico(Guid.NewGuid(), Guid.NewGuid());
        ordemServico.Id = Guid.NewGuid();

        _mockOsRepo.Setup(repo =>
                        repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)))
            .ReturnsAsync(ordemServico);

        var ordemServicoFinalizarExecucaoOrcamentoDto = new OrdemServicoFinalizarExecucaoOrcamentoDto
        {
            OrdemServicoId = ordemServico.Id
        };

        //Act
        await service.FinalizarExecucao(ordemServicoFinalizarExecucaoOrcamentoDto);

        //Assert
        _mockOsRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)));
        Assert.Equal(StatusOrdemServico.Finalizada, ordemServico.Status);
    }

    [Fact]
    public async Task OrdemServicoEntregarVeiculoTest()
    {
        //Arrange
        //ordem servico
        var ordemServico = new OrdemServico(Guid.NewGuid(), Guid.NewGuid());
        ordemServico.Id = Guid.NewGuid();

        _mockOsRepo.Setup(repo =>
                        repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)))
            .ReturnsAsync(ordemServico);

        var ordemServicoEntregarVeiculoDto = new OrdemServicoEntregarVeiculoDto
        {
            OrdemServicoId = ordemServico.Id
        };

        //Act
        await service.EntregarVeiculo(ordemServicoEntregarVeiculoDto);

        //Assert
        _mockOsRepo.Verify(repo => repo.ObterPorId(It.Is<Guid>(guid => guid == ordemServico.Id)));
        Assert.Equal(StatusOrdemServico.Entregue, ordemServico.Status);
    }

}

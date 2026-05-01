using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Application.OrdensServico.DTOs;
using Application.Metricas.DTOs;
using Domain.Estoque.Entities;
using Tests.Integration.Fixture;
using Tests.Integration.Helpers;
using Domain.OrdensServico.Entities;
using Application.Estoque.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;

namespace Tests.Integration.Tests;

public class OrdemServicoIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    private class APIResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
    }

    private class APIMetricaResponse
    {
        public string Message { get; set; }
        public int Segundos { get; set; }
    }

    private class APIMensagemResponse
    {
        public string Message { get; set; }
    }
    
    public OrdemServicoIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
    }

    [Fact]
    public async Task OrdemServico_ObterPorId_DeveRetornarSucesso()
    {
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var ordemServicosList = await _client.GetFromJsonAsync<List<OrdemServicoResponseDto>>("/api/ordemservico/");

        var os = ordemServicosList?.First();

        var response = await _client.GetFromJsonAsync<OrdemServicoResponseDto>($"/api/ordemservico/{os.Id}");

        Assert.Equal(os.Id, response.Id);
    }

    [Fact]
    public async Task OrdemServico_ObterPorIdCliente_DeveRetornarSucesso()
    {
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var ordemServicosList = await _client.GetFromJsonAsync<List<OrdemServicoResponseDto>>("/api/ordemservico/");

        var os = ordemServicosList?.First();

        var response = await _client.GetFromJsonAsync<ListaOrdemServicoResponseDto>($"/api/ordemservico/cliente/{os.ClienteId}");

        var response3 = await _client.GetAsync($"/api/ordemservico/cliente/{os.ClienteId}");
        var content = await response3.Content.ReadAsStringAsync();

        Assert.Equal(os.ClienteId, response.OrdemServicos.First().ClienteId);
    }

    [Fact]
    public async Task OrdemServico_Criar_DeveRetornarSucesso()
    {
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var clientes = await _client.GetFromJsonAsync<List<ClienteResponseDto>>("/api/cliente/");
        var cliente = clientes.FirstOrDefault();

        var veiculos = await _client.GetFromJsonAsync<List<VeiculoResponseDto>>($"/api/veiculo");
        var veiculo = veiculos.FirstOrDefault();

        var dto = new OrdemServicoCreateDto
        {
            ClienteId = cliente.Id,
            VeiculoId = veiculo.Id
        };

        var response = await _client.PostAsJsonAsync("/api/ordemservico/", dto);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task OrdemServico_AdicionarPeca_DeveRetornarSucesso()
    {
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var pecas = await _client.GetFromJsonAsync<List<PecaResponseDto>>("/api/estoque/");
        var peca = pecas.FirstOrDefault();
        var pecaList = new List<OrdemServicoPecaDto>();
        pecaList.Add(new OrdemServicoPecaDto
        {
            PecaId = peca.Id,
            Nome = peca.Nome,
            Preco = peca.Preco,
            Quantidade = 1
        });

        var ordemServicosList = await _client.GetFromJsonAsync<List<OrdemServicoResponseDto>>("/api/ordemservico/");
        var os = ordemServicosList?.First();


        var dto = new OrdemServicoAdicionaPecaDto
        {
            OrdemServicoId = os.Id,
            Pecas = pecaList
        };

        var response = await _client.PostAsJsonAsync("/api/ordemservico/AdicionaPeca", dto);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task OrdemServico_AdicionarServico_DeveRetornarSucesso()
    {
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var servicos = await _client.GetFromJsonAsync<List<ServicoResponseDto>>("/api/servico/");
        var servico = servicos.FirstOrDefault();
        var servicoList = new List<OrdemServicoServicoDto>();
        servicoList.Add(new OrdemServicoServicoDto
        {
            ServicoId = servico.Id,
            Nome = servico.Nome,
            Preco = servico.Preco,
        });

        var ordemServicosList = await _client.GetFromJsonAsync<List<OrdemServicoResponseDto>>("/api/ordemservico/");
        var os = ordemServicosList?.First();

        var dto = new OrdemServicoAdicionaServicoDto
        {
            OrdemServicoId = os.Id,
            Servicos = servicoList
        };

        var response = await _client.PostAsJsonAsync("/api/ordemservico/AdicionaServico", dto);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task OrdemServico_EnviarOrcamento_DeveRetornarSucesso()
    {
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var ordemServicosList = await _client.GetFromJsonAsync<List<OrdemServicoResponseDto>>("/api/ordemservico/");
        var os = ordemServicosList?.First();

        var dto = new OrdemServicoEnviarOrcamentoDto
        {
            OrdemServicoId = os.Id,
        };

        var response = await _client.PostAsJsonAsync("/api/ordemservico/EnviarOrcamento", dto);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task OrdemServico_AprovarOrcamento_DeveRetornarSucesso()
    {
        //Arrange
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var newToken = Guid.NewGuid();
        var ordemServicosList = await _client.GetFromJsonAsync<List<OrdemServicoResponseDto>>("/api/ordemservico/");
        var os = ordemServicosList?.First();
        var dto = new OrdemServicoAprovarOrcamentoDto
        {
            TokenGuid = newToken,
        };

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Tokens.Add(new Token(newToken.ToString("n"), DateTime.Now.AddDays(1), os.Id));
        db.SaveChanges();

        // Act
        var response = await _client.GetFromJsonAsync<APIMensagemResponse>($"/api/ordemservico/AprovarOrcamento/{newToken.ToString("n")}");

        //Assert
        Assert.Contains("aprovado com sucesso", response.Message);
    }

    [Fact]
    public async Task OrdemServico_IniciarDiagnostico_DeveRetornarSucesso()
    {
        //Arrange
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var ordemServicosList = await _client.GetFromJsonAsync<List<OrdemServicoResponseDto>>("/api/ordemservico/");
        var os = ordemServicosList?.First();

        var dto = new OrdemServicoIniciarDiagnosticoOrcamentoDto
        {
            OrdemServicoId = os.Id,
        };

        var response = await _client.PostAsJsonAsync("/api/ordemservico/IniciarDiagnostico", dto);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task OrdemServico_FinalizarDiagnostico_DeveRetornarSucesso()
    {
        //Arrange
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var ordemServicosList = await _client.GetFromJsonAsync<List<OrdemServicoResponseDto>>("/api/ordemservico/");
        var os = ordemServicosList?.First();

        var dto = new OrdemServicoFinalizarDiagnosticoOrcamentoDto
        {
            OrdemServicoId = os.Id,
        };

        var response = await _client.PostAsJsonAsync("/api/ordemservico/FinalizarDiagnostico", dto);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task OrdemServico_IniciarExecucao_DeveRetornarSucesso()
    {
        //Arrange
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var ordemServicosList = await _client.GetFromJsonAsync<List<OrdemServicoResponseDto>>("/api/ordemservico/");
        var os = ordemServicosList?.First();

        var dto = new OrdemServicoIniciarExecucaoOrcamentoDto
        {
            OrdemServicoId = os.Id,
        };

        var response = await _client.PostAsJsonAsync("/api/ordemservico/IniciarExecucao", dto);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task OrdemServico_FinalizarExecucao_DeveRetornarSucesso()
    {
        //Arrange
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var ordemServicosList = await _client.GetFromJsonAsync<List<OrdemServicoResponseDto>>("/api/ordemservico/");
        var os = ordemServicosList?.First();

        var dto = new OrdemServicoFinalizarExecucaoOrcamentoDto
        {
            OrdemServicoId = os.Id,
        };

        var response = await _client.PostAsJsonAsync("/api/ordemservico/FinalizarExecucao", dto);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task OrdemServico_EntregaVeiculo_DeveRetornarSucesso()
    {
        //Arrange
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var ordemServicosList = await _client.GetFromJsonAsync<List<OrdemServicoResponseDto>>("/api/ordemservico/");
        var os = ordemServicosList?.First();

        var dto = new OrdemServicoEntregarVeiculoDto
        {
            OrdemServicoId = os.Id,
        };

        var response = await _client.PostAsJsonAsync("/api/ordemservico/EntregarVeiculo", dto);

        response.EnsureSuccessStatusCode();
    }
}

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Application.Estoque.DTOs;
using Domain.Estoque.Entities;
using Tests.Integration.Fixture;
using Tests.Integration.Helpers;

namespace Tests.Integration.Tests;

public class EstoqueIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EstoqueIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CriarPeca_DeveRetornarSucesso()
    {
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var dto = new
        {
            nome = "Filtro de óleo",
            preco = 50,
            quantidade = 10
        };

        var response = await _client.PostAsJsonAsync("/api/estoque", dto);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        response = await _client.GetAsync("/api/estoque");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("Filtro de óleo", content);
    }

    [Fact]
    public async Task UpdatePeca_DeveRetornarSucesso()
    {
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);
        
        var pecas = await _client.GetFromJsonAsync<List<PecaResponseDto>>("/api/estoque");
        var peca = pecas.FirstOrDefault();

        var dto = new
        {
            nome = "LETRAS",
            preco = 10,
            quantidade = 309
        };

        var response = await _client.PutAsJsonAsync($"/api/estoque/{peca.Id}", dto);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var updatedPeca = await _client.GetFromJsonAsync<PecaResponseDto>($"/api/estoque/{peca.Id}");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal("LETRAS", updatedPeca.Nome);
    }

}

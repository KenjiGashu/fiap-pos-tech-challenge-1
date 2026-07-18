using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Gashu.SistemaMecanica.Application.Estoque.Services;
using Gashu.SistemaMecanica.Application.Metricas.DTOs;
using Gashu.SistemaMecanica.Domain.Estoque.Entities;
using Gashu.SistemaMecanica.Tests.Integration.Fixture;
using Gashu.SistemaMecanica.Tests.Integration.Helpers;

namespace Gashu.SistemaMecanica.Tests.Integration.Tests;

public class MetricasIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

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

    public MetricasIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        factory.SeedDatabase();
    }

    // [Fact]
    // public async Task CriarPeca_DeveRetornarSucesso()
    // {
    //     var token = await AuthHelper.GetToken(_client);
    //     AuthHelper.SetToken(_client, token);

    //     var response = await _client.GetFromJsonAsync<APIMetricaResponse>("/api/metricas/ordemservico/tempo-medio");

    //     Assert.Equal(120, response.Segundos);
    // }

    // [Fact]
    // public async Task UpdatePeca_DeveRetornarSucesso()
    // {
    //     var token = await AuthHelper.GetToken(_client);
    //     AuthHelper.SetToken(_client, token);
        
    //     var pecas = await _client.GetFromJsonAsync<List<PecaResponseDto>>("/api/estoque");
    //     var peca = pecas.FirstOrDefault();

    //     var dto = new
    //     {
    //         nome = "LETRAS",
    //         preco = 10,
    //         quantidade = 109
    //     };

    //     var response = await _client.PutAsJsonAsync($"/api/estoque/{peca.Id}", dto);

    //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    //     var updatedPeca = await _client.GetFromJsonAsync<PecaResponseDto>($"/api/estoque/{peca.Id}");

    //     response.EnsureSuccessStatusCode();

    //     var content = await response.Content.ReadAsStringAsync();

    //     Assert.Equal("LETRAS", updatedPeca.Nome);
    // }

}

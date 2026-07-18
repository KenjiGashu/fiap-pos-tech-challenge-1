using Gashu.SistemaMecanica.Tests.Integration.Fixture;
using System.Net.Http.Json;
using Gashu.SistemaMecanica.Tests.Integration.Helpers;

namespace Gashu.SistemaMecanica.Tests.Integration.Tests;

public class IdentidadeIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public IdentidadeIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _factory.SeedDatabase();
    }

    [Fact]
    public async Task Login_DeveRetornarToken()
    {
        var dto = new
        {
            email = "Admin@gmail.com",
            password = "1234"
        };

        var response = await _client.PostAsJsonAsync("/api/identidade/login", dto);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("token", content);
    }

    [Fact]
    public async Task ObterTodos_DeveRetornarUsuarios()
    {
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var response = await _client.GetAsync("/api/identidade/usuarios");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("Admin@gmail.com", content);
        Assert.Contains("itau@gmail.com", content);
    }

    [Fact]
    public async Task ObterPorEmail_DeveRetornarUsuario()
    {
        var dto = new
        {
            email = "Admin@gmail.com",
            password = "1234",
            roles = new[] { "Admin" }
        };
        
        var token = await AuthHelper.GetToken(_client);
        AuthHelper.SetToken(_client, token);

        var response = await _client.PostAsJsonAsync("/api/identidade/usuarios/", dto);

        response.EnsureSuccessStatusCode();

        response = await _client.GetAsync("/api/identidade/usuarios/");

        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("Admin@gmail.com", content);
    }

}

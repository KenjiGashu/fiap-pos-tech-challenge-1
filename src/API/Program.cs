using Gashu.SistemaMecanica.Application.OrdensServico.Services;
using Gashu.SistemaMecanica.Application.Estoque.Services;
using Gashu.SistemaMecanica.Application.Estoque.Interfaces;
using Gashu.SistemaMecanica.Domain.OrdensServico.Interfaces;
using Gashu.SistemaMecanica.Domain.Estoque.Interfaces;
using Gashu.SistemaMecanica.Infrastructure.Data;
using Gashu.SistemaMecanica.Infrastructure.OrdensServico.Repositories;
using Gashu.SistemaMecanica.Infrastructure.Estoque.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

using Gashu.SistemaMecanica.Domain.Notificacao.Interfaces;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;
using Gashu.SistemaMecanica.Application.Notificacao.Services;
using Gashu.SistemaMecanica.Application.Notificacao.Interfaces;
using Gashu.SistemaMecanica.Infrastructure.Notificacao.Services;
using System.Text;
using Gashu.SistemaMecanica.Application.Identidade.Interfaces;
using Gashu.SistemaMecanica.Application.Identidade.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Gashu.SistemaMecanica.Infrastructure.Identidade.Repositories;
using Gashu.SistemaMecanica.Domain.Identidade.Interfaces;
using Gashu.SistemaMecanica.Domain.Metricas.Interfaces;
using Gashu.SistemaMecanica.Infrastructure.Metricas.Repositories;
using Gashu.SistemaMecanica.Application.Metricas.Interfaces;
using Gashu.SistemaMecanica.Application.Metricas.Services;
using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using Gashu.SistemaMecanica.API.Estoque.Controllers;
using Gashu.SistemaMecanica.API.Estoque.Presenters;
using Gashu.SistemaMecanica.API.Identidade.Controllers;
using Gashu.SistemaMecanica.API.Identidade.Presenters;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsEnvironment("Testing"))
{ 
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("TestDb"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
}

var jwtSecret = Environment.GetEnvironmentVariable("FIAP_POS_SECRET")
    ?? throw new InvalidOperationException(
        "Environment variable FIAP_POS_SECRET was not configured.");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("FIAP_POS_SECRET"))
            )
        };
    });

builder.Services.AddAuthorization();

//repository
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<IPecaRepository, PecaRepository>();

//estoque
builder.Services.AddScoped<IEstoqueService, EstoqueService>();
builder.Services.AddScoped<IEstoqueController, EstoqueController>();
builder.Services.AddScoped<IEstoquePresenter, EstoquePresenterDAO>();

//clientes e veiculos 
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();

//servico
builder.Services.AddScoped<IServicoService, ServicoService>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();

//ordem servico
builder.Services.AddScoped<IOrdemServicoService, OrdemServicoService>();
builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();

// token
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

//Notificacao
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();
builder.Services.AddScoped<ICanalNotificacao, CanalNotificacaoEmail>();

//autenticacao
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IIdentidadeService, IdentidadeService>();
builder.Services.AddScoped<IIdentidadeController, IdentidadeController>();
builder.Services.AddScoped<IIdentidadePresenter, IdentidadePresenterDAO>();
builder.Services.AddScoped<IJwtService, JwtService>();

//metricas
builder.Services.AddScoped<IMetricaOrdemServicoRepository, MetricaOrdemServicoRepository>();
builder.Services.AddScoped<IMetricaOrdemServicoService, MetricaOrdemServicoService>();

builder.Services.AddControllers();


//health check do kubernetes
builder.Services.AddHealthChecks();

// swagger
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// ativar healthcheck do kubernetes
app.MapHealthChecks("/health");

// Ativar Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await DbInitializer.SeedAsync(context);
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

// for production, to avoid leaking internal information when exception occurs
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var response = new
        {
            Message = "Ocorreu um erro interno."
        };

        await context.Response.WriteAsJsonAsync(response);
    });
});

app.Run();

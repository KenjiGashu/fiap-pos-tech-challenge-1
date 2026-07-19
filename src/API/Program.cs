using Gashu.SistemaMecanica.Application.OrdensServico.Services;
using Gashu.SistemaMecanica.Application.Estoque.Services;
using Gashu.SistemaMecanica.Application.Repositories;
using Gashu.SistemaMecanica.Application.Repositories;
using Gashu.SistemaMecanica.Infrastructure.Data;
using Gashu.SistemaMecanica.Infrastructure.OrdensServico.Repositories;
using Gashu.SistemaMecanica.Infrastructure.Estoque.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

using Gashu.SistemaMecanica.Application.Gateway;
using Gashu.SistemaMecanica.Application.Notificacao.Services;
using Gashu.SistemaMecanica.Infrastructure.Notificacao.Services;
using System.Text;
using Gashu.SistemaMecanica.Application.Identidade.Services;
using Microsoft.IdentityModel.Tokens;
using Gashu.SistemaMecanica.Infrastructure.Identidade.Repositories;
using Gashu.SistemaMecanica.Application.Repositories;
using Gashu.SistemaMecanica.Application.Repositories;
using Gashu.SistemaMecanica.Infrastructure.Metricas.Repositories;
using Gashu.SistemaMecanica.Application.Metricas.Services;
using Gashu.SistemaMecanica.Application.OrdensServico.Interfaces;
using Gashu.SistemaMecanica.Application.OrdensServico.Services;
using System.Reflection;
using Gashu.SistemaMecanica.API.Estoque.Controllers;
using Gashu.SistemaMecanica.API.Estoque.Presenters;
using Gashu.SistemaMecanica.API.Identidade.Controllers;
using Gashu.SistemaMecanica.API.Identidade.Presenters;
using Gashu.SistemaMecanica.API.Metricas.Controllers;
using Gashu.SistemaMecanica.API.Metricas.Presenters;
using Gashu.SistemaMecanica.API.OrdensServico.Controllers;
using Gashu.SistemaMecanica.API.OrdensServico.Presenters;

using System.Diagnostics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

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

// Configure OpenTelemetry with tracing and auto-start.
// builder.Services.AddOpenTelemetry()
//     .ConfigureResource(resource => resource
//         .AddService(serviceName: builder.Environment.ApplicationName))
//     .WithTracing(tracing => tracing
//         .AddAspNetCoreInstrumentation()
//         .AddConsoleExporter());

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
        resource.AddService(
            serviceName: "MinhaApi",
            serviceVersion: "1.0.0"))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://otel-collector:4317");
            })
            .AddConsoleExporter();
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://otel-collector:4317");
            })
            .AddConsoleExporter();
    });


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

//estoque
builder.Services.AddScoped<IEstoqueService, EstoqueService>();
builder.Services.AddScoped<IEstoqueController, EstoqueController>();
builder.Services.AddScoped<IEstoquePresenter, EstoquePresenterDAO>();
builder.Services.AddScoped<IPecaRepository, PecaRepository>();

//clientes 
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteController, ClienteController>();
builder.Services.AddScoped<IClientePresenter, ClientePresenter>();

// veiculos
builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<IVeiculoController, VeiculoController>();
builder.Services.AddScoped<IVeiculoPresenter, VeiculoPresenter>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();

//servico
builder.Services.AddScoped<IServicoService, ServicoService>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<IServicoController, ServicoController>();
builder.Services.AddScoped<IServicoPresenter, ServicoPresenter>();

//ordem servico
builder.Services.AddScoped<IOrdemServicoService, OrdemServicoService>();
builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
builder.Services.AddScoped<IOrdemServicoPresenter, OrdemServicoPresenter>();
builder.Services.AddScoped<IOrdemServicoController, OrdemServicoController>();

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
builder.Services.AddScoped<IMetricasController, MetricasController>();
builder.Services.AddScoped<IMetricasPresenter, MetricasPresenter>();

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
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
//}

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

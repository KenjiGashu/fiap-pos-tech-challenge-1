using Application.OrdensServico.Services;
using Application.Estoque.Services;
using Application.Estoque.Interfaces;
using Domain.OrdensServico.Interfaces;
using Domain.Estoque.Interfaces;
using Infrastructure.Data;
using Infrastructure.OrdensServico.Repositories;
using Infrastructure.Estoque.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

using Domain.Notificacao.Interfaces;
using Application.OrdensServico.Interfaces;
using Application.Notificacao.Services;
using Application.Notificacao.Interfaces;
using Infrastructure.Notificacao.Services;
using System.Text;
using Application.Identidade.Interfaces;
using Application.Identidade.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Identidade.Repositories;
using Domain.Identidade.Interfaces;
using Domain.Metricas.Interfaces;
using Infrastructure.Metricas.Repositories;
using Application.Metricas.Interfaces;
using Application.Metricas.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=clientes.db"));

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
                Encoding.ASCII.GetBytes("minha_chave_super_secreta_com_32_chars!!")
            )
        };
    });

builder.Services.AddAuthorization();

//repository
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<IPecaRepository, PecaRepository>();

//pecas, clientes e veiculos 
builder.Services.AddScoped<IEstoqueService, EstoqueService>();
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
builder.Services.AddScoped<IJwtService, JwtService>();

//metricas
builder.Services.AddScoped<IMetricaOrdemServicoRepository, MetricaOrdemServicoRepository>();
builder.Services.AddScoped<IMetricaOrdemServicoService, MetricaOrdemServicoService>();

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await context.Database.EnsureCreatedAsync();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

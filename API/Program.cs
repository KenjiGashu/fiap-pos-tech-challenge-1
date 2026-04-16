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

using Application.Teste.Services;
using Infrastructure.Teste.Repositories;
using Domain.Teste.Interfaces;

using Domain.Notificacao.Entities;
using Domain.Notificacao.Interfaces;
using Infrastructure.Notificacao.Repositories;
using Application.Notificacao.Service;
using Application.Notificacao.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=clientes.db"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<IPecaRepository, PecaRepository>();
builder.Services.AddScoped<IEstoqueService, EstoqueService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<EstoqueService>();
builder.Services.AddScoped<VeiculoService>();

//servico
builder.Services.AddScoped<ServicoService>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();

//ordem servico
builder.Services.AddScoped<OrdemServicoService>();
builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();

//Pessoa (Teste)
builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();

//Notificacao
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();
builder.Services.AddScoped<INotificacaoRepository, NotificacaoRepository>();

builder.Services.AddControllers();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await context.Database.EnsureCreatedAsync();
}

app.MapControllers();

app.Run();

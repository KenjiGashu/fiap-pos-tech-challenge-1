using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Infrastructure.Data;
using Domain.Identidade.Entities;
using System.Security.Cryptography;
using Domain.OrdensServico.Entities;
using Domain.Estoque.Entities;
using Domain.Metricas.Entities;
using Application.Notificacao.Interfaces;
using Tests.Integration.FakeServices;
using Application.Estoque.Interfaces;
using Application.Estoque.Services;

namespace Tests.Integration.Fixture;

public class CustomWebApplicationFactory<TProgram>
: WebApplicationFactory<TProgram> where TProgram : class
{
    string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            100000,
            HashAlgorithmName.SHA256,
            32
        );

        var hashBytes = new byte[48];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
        Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);

        return Convert.ToBase64String(hashBytes);
    }

    public CustomWebApplicationFactory()
    {
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        context.Database.EnsureCreated();
        
        // Seed Database
        //Usuario
        if (context.Set<Usuario>().Count() <= 0)
        {
            var hashedPassword = HashPassword("1234");
            var usuario = new Usuario("Admin@gmail.com", hashedPassword);
            var role = new Role("Admin");
            usuario.Roles.Add(role);
            context.Set<Usuario>().Add(usuario);

            hashedPassword = HashPassword("1234");
            usuario = new Usuario("maria@gmail.com", hashedPassword);
            role = new Role("Cliente");
            usuario.Roles.Add(role);
            context.Set<Usuario>().Add(usuario);

            hashedPassword = HashPassword("1234");
            usuario = new Usuario("jose@gmail.com", hashedPassword);
            role = new Role("Cliente");
            usuario.Roles.Add(role);
            context.Set<Usuario>().Add(usuario);

            hashedPassword = HashPassword("1234");
            usuario = new Usuario("itau@gmail.com", hashedPassword);
            role = new Role("Cliente");
            usuario.Roles.Add(role);
            context.Set<Usuario>().Add(usuario);

            context.SaveChanges();
        }
            
        if (context.Set<Cliente>().Count() <= 0)
        {
            var cliente = new Cliente("maria", "200.766.210-81", "", TipoPessoa.Fisica);
            var cliente2 = new Cliente("jose", "848.288.680-03", "", TipoPessoa.Fisica);
            var cliente3 = new Cliente("itau", "", "60.701.190/0001-04", TipoPessoa.Juridica);
            var clienteSet = context.Set<Cliente>();
            var usuario =  context.Set<Usuario>().FirstOrDefault(u => u.Email == "maria@gmail.com");
            cliente.UsuarioId = usuario.Id;
            usuario =  context.Set<Usuario>().FirstOrDefault(u => u.Email == "jose@gmail.com");
            cliente2.UsuarioId = usuario.Id;
            usuario =  context.Set<Usuario>().FirstOrDefault(u => u.Email == "itau@gmail.com");
            cliente3.UsuarioId = usuario.Id;
            clienteSet.Add(cliente);
            clienteSet.Add(cliente2);
            clienteSet.Add(cliente3);
            context.SaveChanges();
        }

        if (context.Set<Veiculo>().Count() <= 0)
        {
            var veiculo = new Veiculo("HVV-0109", "marca1", "modelo1", 2010);
            var veiculo2 = new Veiculo("MRG-8829", "marca2", "modelo2", 2021);
            var veiculo3 = new Veiculo("TKJ5A20", "marca3", "modelo3", 2026);
            context.Set<Veiculo>().Add(veiculo);
            context.Set<Veiculo>().Add(veiculo2);
            context.Set<Veiculo>().Add(veiculo3);
            context.SaveChanges();

        }

        if (context.Set<Peca>().Count() <= 0)
        {
            var peca = new Peca("Pneu", 150, 150);
            var peca2 = new Peca("Rodo de Retrovisor", 40, 150);
            var peca3 = new Peca("Vidro de Retrovisor", 200, 150);
            context.Set<Peca>().Add(peca);
            context.Set<Peca>().Add(peca2);
            context.Set<Peca>().Add(peca3);
            context.SaveChanges();

        }

        if (context.Set<Servico>().Count() <= 0)
        {
            var servico = new Servico("Alinhamento", 150);
            var servico2 = new Servico("Troca de Pneu", 50);
            var servico3 = new Servico("Troca Bateria", 200);
            context.Set<Servico>().Add(servico);
            context.Set<Servico>().Add(servico2);
            context.Set<Servico>().Add(servico3);
            context.SaveChanges();

        }

        if (context.Set<OrdemServico>().Count() <= 0)
        {
            var cliente = context.Set<Cliente>().First();
            var veiculo = context.Set<Veiculo>().First();
            var ordemServico = new OrdemServico(cliente.Id, veiculo.Id);
            var peca = context.Set<Peca>().First();
            var servico = context.Set<Servico>().First();

            ordemServico.AdicionarPeca(peca.Id, peca.Preco, 1, peca.Nome);
            ordemServico.AdicionarServico(servico.Id, servico.Preco, servico.Nome);
            context.Set<OrdemServico>().Add(ordemServico);
            context.SaveChanges();
        }

        if (context.Set<MetricaOrdemServico>().Count() <= 0)
        {
            var id = Guid.NewGuid();
            var date = DateTime.Now;
            var recebida = new MetricaOrdemServico(id, global::Domain.Metricas.Entities.StatusOrdemServico.Recebida, date);
            context.Set<MetricaOrdemServico>().Add(recebida);
            var date2 = date.AddSeconds(15);
            var aguardando = new MetricaOrdemServico(id, global::Domain.Metricas.Entities.StatusOrdemServico.AguardandoAprovacao, date2);
            var date3 = date2.AddSeconds(15);
            context.Set<MetricaOrdemServico>().Add(aguardando);
            var aprovado = new MetricaOrdemServico(id, global::Domain.Metricas.Entities.StatusOrdemServico.OrcamentoAprovado, date3);
            var date4 = date3.AddSeconds(15);
            context.Set<MetricaOrdemServico>().Add(aprovado);
            var emDiagnostico = new MetricaOrdemServico(id, global::Domain.Metricas.Entities.StatusOrdemServico.EmDiagnostico, date4);
            var date5 = date4.AddSeconds(15);
            context.Set<MetricaOrdemServico>().Add(emDiagnostico);
            var aguardandoMecanico = new MetricaOrdemServico(id, global::Domain.Metricas.Entities.StatusOrdemServico.AguardandoMecanico, date5);
            var date6 = date5.AddSeconds(15);
            context.Set<MetricaOrdemServico>().Add(aguardandoMecanico);
            var emExecucao = new MetricaOrdemServico(id, global::Domain.Metricas.Entities.StatusOrdemServico.EmExecucao, date6);
            var date7 = date6.AddSeconds(15);
            context.Set<MetricaOrdemServico>().Add(emExecucao);
            var finalizado = new MetricaOrdemServico(id, global::Domain.Metricas.Entities.StatusOrdemServico.Finalizada, date7);
            var date8 = date7.AddSeconds(15);
            context.Set<MetricaOrdemServico>().Add(finalizado);
            var entregue = new MetricaOrdemServico(id, global::Domain.Metricas.Entities.StatusOrdemServico.Entregue, date8.AddSeconds(15));
            context.Set<MetricaOrdemServico>().Add(entregue);
            context.SaveChanges();
        }

        // tokens
        if (context.Set<Token>().Count() <= 0)
        {
            var newToken = new Token(Guid.NewGuid().ToString("n"), DateTime.Now.AddDays(1), Guid.NewGuid());
            context.Set<Token>().Add(newToken);
            context.SaveChanges();
        }

        context.SaveChanges();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing"); // 🔥 importante

        builder.ConfigureServices(services =>
        {
            // remove o DbContext original
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            // adiciona InMemory FIXO (mesmo banco pra todos)
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            services.AddScoped<INotificacaoService, NotificacaoFakeService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
        });
    }
}

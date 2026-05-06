using Microsoft.EntityFrameworkCore;
using Gashu.SistemaMecanica.Domain.OrdensServico.Entities;
using Gashu.SistemaMecanica.Domain.Estoque.Entities;
using Gashu.SistemaMecanica.Domain.Identidade.Entities;
using System.Security.Cryptography;
using Gashu.SistemaMecanica.Application.Identidade.Services;
using Gashu.SistemaMecanica.Domain.Metricas.Entities;

namespace Gashu.SistemaMecanica.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Servico> Servicos { get; set; }
    public DbSet<Peca> Pecas { get; set; }

    public DbSet<OrdemServico> OrdemServicos { get; set; }
    public DbSet<OrdemServicoServico> OrdemServicoServicos { get; set; }
    public DbSet<OrdemServicoPeca> OrdemServicoPecas { get; set; }

    //Autenticacao
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Role> Roles { get; set; }

    // Notificacao
    public DbSet<Token> Tokens { get; set; }

    // Metricas
    public DbSet<MetricaOrdemServico> Metricas { get; set; }

    private string hashPassword(string password)
    {
        // gera senha com hash
        var salt = RandomNumberGenerator.GetBytes(16);
      
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            100000,
            HashAlgorithmName.SHA256,
            32
        );
                
        // junta salt + hash
        var hashBytes = new byte[48];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
        Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);
                
        var hashedPassword =  Convert.ToBase64String(hashBytes);
        
        return hashedPassword;
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Peca>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Servico>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Veiculo>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Cliente>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<OrdemServico>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<OrdemServicoPeca>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<OrdemServicoServico>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Usuario>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Role>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        // Ordem -> Pecas
        modelBuilder.Entity<OrdemServico>()
            .HasMany(o => o.OrdemServicoPecas)
            .WithOne(p => p.OrdemServico)
            .HasForeignKey(p => p.OrdemServicoId);

        // Ordem -> Servicos
        modelBuilder.Entity<OrdemServico>()
            .HasMany(o => o.OrdemServicoServicos)
            .WithOne(s => s.OrdemServico)
            .HasForeignKey(s => s.OrdemServicoId);

        // Peca -> OrdemServicoPeca
        modelBuilder.Entity<OrdemServicoPeca>()
            .HasOne(op => op.Peca)
            .WithMany(p => p.OrdemServicoPecas)
            .HasForeignKey(op => op.PecaId);
                            
        // Servico -> OrdemServicoServico
        modelBuilder.Entity<OrdemServicoServico>()
            .HasOne(oss => oss.Servico)
            .WithMany(s => s.OrdemServicoServicos)
            .HasForeignKey(oss => oss.ServicoId);

        //Autenticacao
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Usuarios);
    }
}

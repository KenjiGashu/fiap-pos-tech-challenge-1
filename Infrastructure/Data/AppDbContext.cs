using Microsoft.EntityFrameworkCore;
using Domain.OrdensServico.Entities;
using Domain.Estoque.Entities;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
		public DbSet<Veiculo> Veiculos { get; set; }
		public DbSet<Servico> Servicos { get; set; }
		public DbSet<Peca> Pecas { get; set; }

		public DbSet<OrdemServico> OrdemServicos { get; set; }
		public DbSet<OrdemServicoServico> OrdemServicoServicos { get; set; }
		public DbSet<OrdemServicoPeca> OrdemServicoPecas { get; set; }

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

                entity.Property(c => c.Email)
                      .IsRequired()
                      .HasMaxLength(150);
            });

						base.OnModelCreating(modelBuilder);

            // OrdemServico
            modelBuilder.Entity<OrdemServico>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OrdemServico>()
                .HasMany(o => o.Servicos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrdemServico>()
                .HasMany(o => o.Pecas)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // OrdemServicoServico
            modelBuilder.Entity<OrdemServicoServico>()
                .HasKey(s => s.Id);

            // OrdemServicoPeca
            modelBuilder.Entity<OrdemServicoPeca>()
                .HasKey(p => p.Id);

    }
}

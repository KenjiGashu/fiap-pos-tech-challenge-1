using Microsoft.EntityFrameworkCore;
using Domain.OrdensServico.Entities;
using Domain.Estoque.Entities;
using Domain.Teste.Entities;

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

		// Notificacao
		public DbSet<Token> Tokens { get; set; }

    // teste
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
		
		//blogs
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
=> optionsBuilder
          .UseSqlite("Data Source=clientes.db")
    .UseAsyncSeeding(async (context, _, cancellationToken) =>
    {
        if (context.Set<Cliente>().Count() <= 0)
        {
            var cliente = new Cliente("maria", "maria@gmail.com", "200.766.210-81", "", TipoPessoa.Fisica);
            var cliente2 = new Cliente("jose", "jose@gmail.com", "848.288.680-03", "", TipoPessoa.Fisica);
            var cliente3 = new Cliente("itau", "itau@gmail.com", "", "60.701.190/0001-04", TipoPessoa.Juridica);
            var clienteSet = context.Set<Cliente>();
            await clienteSet.AddAsync(cliente);
            await clienteSet.AddAsync(cliente2);
            await clienteSet.AddAsync(cliente3);
            await context.SaveChangesAsync();
        }

        if (context.Set<Veiculo>().Count() <= 0)
        {
            var veiculo = new Veiculo("HVV-0109", "marca1", "modelo1", 2010);
            var veiculo2 = new Veiculo("MRG-8829", "marca2", "modelo2", 2021);
            var veiculo3 = new Veiculo("TKJ5A20", "marca3", "modelo3", 2026);
            await context.Set<Veiculo>().AddAsync(veiculo);
            await context.Set<Veiculo>().AddAsync(veiculo2);
            await context.Set<Veiculo>().AddAsync(veiculo3);
            await context.SaveChangesAsync();

        }

        if (context.Set<Peca>().Count() <= 0)
        {
            var peca = new Peca("Pneu", 150, 150);
            var peca2 = new Peca("Rodo de Retrovisor", 40, 150);
            var peca3 = new Peca("Vidro de Retrovisor", 200, 150);
            await context.Set<Peca>().AddAsync(peca);
            await context.Set<Peca>().AddAsync(peca2);
            await context.Set<Peca>().AddAsync(peca3);
            await context.SaveChangesAsync();

        }

        if (context.Set<Servico>().Count() <= 0)
        {
            var servico = new Servico("Alinhamento", 150);
            var servico2 = new Servico("Troca de Pneu", 50);
            var servico3 = new Servico("Troca Bateria", 200);
            await context.Set<Servico>().AddAsync(servico);
            await context.Set<Servico>().AddAsync(servico2);
            await context.Set<Servico>().AddAsync(servico3);
            await context.SaveChangesAsync();

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
            await context.Set<OrdemServico>().AddAsync(ordemServico);
            await context.SaveChangesAsync();
        }
    })
        .UseSeeding((context, _) =>
    {
        if (context.Set<Cliente>().Count() <= 0)
        {
            var cliente = new Cliente("maria", "maria@gmail.com", "200.766.210-81", "", TipoPessoa.Fisica);
            var cliente2 = new Cliente("jose", "jose@gmail.com", "848.288.680-03", "", TipoPessoa.Fisica);
            var cliente3 = new Cliente("itau", "itau@gmail.com", "", "60.701.190/0001-04", TipoPessoa.Juridica);
            var clienteSet = context.Set<Cliente>();
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
    });

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
            .HasOne(op => op.Servico)
            .WithMany(p => p.OrdemServicoServicos)
            .HasForeignKey(op => op.ServicoId);

        // teste
        modelBuilder.Entity<Pessoa>()
					.HasMany(p => p.Pedidos)
					.WithOne(p => p.Pessoa)
					.HasForeignKey(p => p.PessoaId)
					.IsRequired();


				// Blogs
				modelBuilder.Entity<Blog>()
					.HasMany(b => b.Posts)
					.WithOne(p => p.Blog)
					.HasForeignKey(p => p.BlogId);
    }
}

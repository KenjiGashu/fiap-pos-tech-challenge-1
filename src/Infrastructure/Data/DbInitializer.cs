using System.Security.Cryptography;
using Domain.Estoque.Entities;
using Domain.Identidade.Entities;
using Domain.OrdensServico.Entities;
using Infrastructure.Data;

public static class DbInitializer
{
    private static string hashPassword(string password)
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
    
    public static async Task SeedAsync(AppDbContext context)
    {
        // garante que banco existe
        await context.Database.EnsureCreatedAsync();
        
        //Usuario
        if (context.Set<Usuario>().Count() <= 0)
        {
            var hashedPassword = hashPassword("1234");
            var usuario = new Usuario("Admin@gmail.com", hashedPassword);
            var role = new Role("Admin");
            usuario.Roles.Add(role);
            context.Set<Usuario>().Add(usuario);

            hashedPassword = hashPassword("1234");
            usuario = new Usuario("maria@gmail.com", hashedPassword);
            role = new Role("Cliente");
            usuario.Roles.Add(role);
            context.Set<Usuario>().Add(usuario);

            hashedPassword = hashPassword("1234");
            usuario = new Usuario("jose@gmail.com", hashedPassword);
            role = new Role("Cliente");
            usuario.Roles.Add(role);
            context.Set<Usuario>().Add(usuario);

            hashedPassword = hashPassword("1234");
            usuario = new Usuario("itau@gmail.com", hashedPassword);
            role = new Role("Cliente");
            usuario.Roles.Add(role);
            context.Set<Usuario>().Add(usuario);

            await context.SaveChangesAsync();
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

            
    }
        
}

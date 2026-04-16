using Domain.Teste.Entities;
using Domain.Teste.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Teste.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _context;

    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pessoa>> ObterTodos()
        => await _context.Pessoas.ToListAsync();

    public async Task<IEnumerable<Blog>> ObterTodosBlogs()
			=> await _context.Blogs.Include(b => b.Posts).ToListAsync();


    public async Task<Pessoa?> ObterPorId(Guid id)
        => await _context.Pessoas.FindAsync(id);

    public async Task Adicionar(Pessoa pessoa)
    {
        await _context.Pessoas.AddAsync(pessoa);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Pessoa pessoa)
    {
        _context.Pessoas.Update(pessoa);
        await _context.SaveChangesAsync();
    }

    public async Task Remover(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa != null)
        {
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
        }
    }

		public async Task AdicionaPedido(Guid pessoaId, Pedido novoPedido)
		{

			
             var blog = _context.Blogs.Find(1);
			
						 if(blog == null)
						 {
							 blog = new Blog { Name = "Meu Blog" };
							 _context.Blogs.Add(blog);
							 _context.SaveChanges();
						 }

            // Adicionar posts diretamente via FK
            var post1 = new Post
            {
                Title = "Primeiro Post",
                Content = "Conteúdo do primeiro post",
                BlogId = blog.Id
            };

            _context.Posts.Add(post1);
            _context.SaveChanges();

            // Outra forma: adicionar via navegação
            var post2 = new Post
            {
                Title = "Segundo Post",
                Content = "Conteúdo do segundo post"
            };

            blog.Posts.Add(post2);
            _context.SaveChanges();



            // Buscar blog com posts
            var blogs = _context.Blogs
                .Include(b => b.Posts)
                .ToList();

            foreach (var b in blogs)
            {
                Console.WriteLine($"Blog: {b.Name} {b.Id}");
                foreach (var p in b.Posts)
                {
                    Console.WriteLine($" - Post: {p.Title} {p.Id}");
                }
            }


        // var pessoa = _context.Pessoas
        //     .Include(p => p.Pedidos)
        //     .FirstOrDefault(p => p.PessoaId == pessoaId);

        var pessoa = _context.Pessoas.Find("00000000-0000-0000-0000-000000000000");

        var pedido = new Pedido { Data = DateTime.Now };
        pessoa.Pedidos.Add(pedido);

        _context.SaveChanges();
        // var p = await _context.Pessoas.Include(b => b.Pedidos).FirstAsync();
        // var pedido = new Pedido { Data = DateTime.Now };

        // p.Pedidos.Add(pedido);
        // await _context.SaveChangesAsync();




        // var pessoa2 = _context.Pessoas
        //     .FirstOrDefault(p => p.PessoaId == pessoaId);

        // Console.WriteLine($"[AdicionaPedido] {pessoa2}");

        //var pessoa3 = await _context.Pessoas.FindAsync(pessoaId);

        // Console.WriteLine($"[AdicionaPedido] pessoa3 {pessoa3}");

        // var pessoa4 = await _context.Pessoas.ToListAsync();

        // foreach(var p in pessoa4)
        // {
        //     Console.WriteLine($"[gashu] p {p.PessoaId}");
        // }

        // if (pessoa == null)
        //     throw new Exception("Cliente não encontrado");

        ///pessoa.Pedidos.Add(novoPedido);
        // novoPedido.PessoaId = pessoaId;

        // _context.Entry(novoPedido).State = EntityState.Added;

        // pessoa?.Pedidos.Add(novoPedido);

        // await _context.SaveChangesAsync();
    }

		public async Task AdicionaPedidos(Guid pessoaId, IEnumerable<Pedido> novoPedidos)
		{
        var pessoa = _context.Pessoas
            .Include(p => p.Pedidos)
            .FirstOrDefault(p => p.PessoaId == pessoaId);

        if (pessoa == null)
            throw new Exception("Cliente não encontrado");

				foreach( var pedido in novoPedidos)
				{
					pessoa.Pedidos.Add(pedido);
				}
				
        _context.SaveChanges();
		}

}

namespace Domain.Teste.Entities;

public class Pessoa
{
    public Guid PessoaId { get; set; }
    public string Nome { get; set; }

    // 1 Cliente -> N Pedidos
    public List<Pedido> Pedidos { get; set; } = new();

    public void Atualizar(string nome){
        this.Nome = nome;
    }
}

public class Pedido
{
    public Guid PedidoId { get; set; }
    public DateTime Data { get; set; }

    // Chave estrangeira
    public Guid PessoaId { get; set; }

    // Navegação
    public Pessoa Pessoa { get; set; }
}

public class Blog
{
	public int Id { get; set; }
	public string Name { get; set; }

	// One-to-many relationship
	public List<Post> Posts { get; set; } = new List<Post>();
}

public class Post
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }

	public int BlogId { get; set; }
	public Blog Blog { get; set; }
}

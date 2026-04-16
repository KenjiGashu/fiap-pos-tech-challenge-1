namespace Application.Teste.DTOs;


public class PedidoDto
{
    public Guid PedidoId { get; set; }
    public DateTime Data { get; set; }

    // // Chave estrangeira
    // public int PessoaId { get; set; }

    // // Navegação
    // public PessoaDto Pessoa { get; set; }
}

public class PessoaDto
{
    public Guid PessoaId { get; set; }
    public string Nome { get; set; }

    // 1 Cliente -> N Pedidos
    public List<PedidoDto> Pedidos { get; set; }

}

public class PessoaResponseDto
{
    public Guid PessoaId { get; set; }
    public string Nome { get; set; }

    // 1 Cliente -> N Pedidos
    public List<PedidoDto> Pedidos { get; set; }

}

public class PessoaAdicionaPedidoDto
{
	public Guid PessoaId { get; set; }
	public PedidoDto pedido { get; set; }
}

public class PessoaAdicionaPedidosDto
{
	public Guid PessoaId { get; set; }
	public IEnumerable<PedidoDto> pedidos { get; set; }
}

public class BlogDto
{
	public int Id { get; set; }
	public string Name { get; set; }

	// One-to-many relationship
	public List<PostDto> Posts { get; set; } = new List<PostDto>();
}

public class PostDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }

	public int BlogId { get; set; }
	public BlogDto Blog { get; set; }
}

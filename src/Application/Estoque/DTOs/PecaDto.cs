namespace Application.Estoque.DTOs;

public class PecaCreateDto
{
    public required string Nome { get; set; }
    public required decimal Preco { get; set; }
    public required int Quantidade {get;set;}
}

public class PecaResponseDto
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required decimal Preco { get; set; }
	public required int Quantidade {get;set;}
}

public class PecaUpdateDto
{
    public required string Nome { get; set; }
    public required decimal Preco { get; set; }
	public required int Quantidade {get;set;}
}

namespace Application.Estoque.DTOs;

public class PecaCreateDto
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
	  public int Quantidade {get;set;}
}

public class PecaResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
	public int Quantidade {get;set;}
}

public class PecaUpdateDto
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
	public int Quantidade {get;set;}
}

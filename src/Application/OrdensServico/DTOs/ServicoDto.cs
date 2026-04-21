namespace Application.OrdensServico.DTOs;

public class ServicoCreateDto
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}

public class ServicoResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}

public class ServicoUpdateDto
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}

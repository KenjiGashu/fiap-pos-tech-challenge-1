namespace Application.Notificacao.DTOs;

public class ItemOrcamentoDto
{
	public string Nome { get; set; }
	public decimal Preco { get; set; }
	public int Quantidade { get; set; }

	public ItemOrcamentoDto(string nome, decimal preco, int quantidade)
	{
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
    }
}

public class AprovacaoOrcamentoDto
{
	  public Guid OrdemServicoId { get; set; }
	  public string Token { get; set; }
	  public IEnumerable<ItemOrcamentoDto> Servicos { get; set; }
	  public IEnumerable<ItemOrcamentoDto> Pecas { get; set; }
	  public decimal Total { get; set; }
	  public string NomeCliente { get; set; }
	  public string Destinatario { get; set; }

}

public class AprovarOrcamentoDto
{
    public Guid Guid;
}

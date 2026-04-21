namespace Domain.Notificacao.Entities;

public class ItemOrcamento
{
	public string Nome { get; set; }
	public decimal Preco { get; set; }
	public int Quantidade { get; set; }

	  public ItemOrcamento(string nome, decimal preco, int quantidade)
	  {
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
    }
}

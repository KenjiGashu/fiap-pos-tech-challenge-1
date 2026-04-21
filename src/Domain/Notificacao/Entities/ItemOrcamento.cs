namespace Domain.Notificacao.Entities;

public enum TipoItemOrcamento
{
	Servico,
	Peca
}

public class ItemOrcamento
{
	public string Nome { get; set; }
	public decimal Preco { get; set; }
	public int Quantidade { get; set; }
	public TipoItemOrcamento Tipo { get; set; }

	public ItemOrcamento(string nome, decimal preco, int quantidade, TipoItemOrcamento tipo)
	  {
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
        Tipo = tipo;
    }
}

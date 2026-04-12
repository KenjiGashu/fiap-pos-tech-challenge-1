namespace Domain.Estoque.Entities;

public class Peca
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public decimal Preco { get; private set; }
	  public int Quantidade {get; private set;}

    protected Peca() { }

	  public Peca(string nome, decimal preco, int quantidade)
    {
        Id = Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("Nome obrigatório");

        if (preco < 0)
            throw new Exception("Preço inválido");

				if(quantidade < 0)
					throw new Exception("Quantidade invalida");

        Nome = nome;
        Preco = preco;
				Quantidade = quantidade;
    }

	  public void Atualizar(string nome, decimal preco, int quantidade)
    {
        Nome = nome;
        Preco = preco;
				Quantidade = quantidade;
    }

	  public void Adicionar(int quantidade)
	  {
		  if(quantidade <= 0)
		  	throw new Exception("Quantidade invalida");

	  	this.Quantidade += quantidade;
	  }

	  public void Consumir(int quantidade)
  	{
	  	if(quantidade <= 0)
	  		throw new Exception("quantidade invalida");
		
	  	if(quantidade > this.Quantidade)
	  		throw new Exception("nao ha estoque suficiente");

		  this.Quantidade -= quantidade;
  	}
}

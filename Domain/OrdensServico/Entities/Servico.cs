namespace Domain.OrdensServico.Entities;

public class Servico
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public decimal Preco { get; private set; }

    protected Servico() { }

    public Servico(string nome, decimal preco)
    {
        Id = Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("Nome obrigatório");

        if (preco < 0)
            throw new Exception("Preço inválido");

        Nome = nome;
        Preco = preco;
    }

    public void Atualizar(string nome, decimal preco)
    {
        Nome = nome;
        Preco = preco;
    }
}

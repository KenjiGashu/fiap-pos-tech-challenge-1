namespace Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public class Servico
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }

    public List<OrdemServicoServico> OrdemServicoServicos { get; set; }
	
    protected Servico() { }

    public Servico(string nome, decimal preco)
    {
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

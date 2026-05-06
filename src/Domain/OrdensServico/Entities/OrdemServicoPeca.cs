namespace Domain.OrdensServico.Entities;
using Domain.Estoque.Entities;

public class OrdemServicoPeca
{
    public Guid Id { get; set; }

	  public Guid OrdemServicoId { get; set; }
	  public OrdemServico OrdemServico { get; set; }

    public Guid PecaId { get; set; }
    public Peca Peca { get; set; } = null!;

    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }

	  public OrdemServicoPeca(Guid ordemServicoId, Guid pecaId, string nome, decimal preco, int quantidade)
    {
        OrdemServicoId = ordemServicoId;
        Nome = nome;
        PecaId = pecaId;
        Preco = preco;
        Quantidade = quantidade;
    }
}

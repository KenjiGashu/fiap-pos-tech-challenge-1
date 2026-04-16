namespace Domain.OrdensServico.Entities;
using Domain.Estoque.Entities;

public class OrdemServicoPeca
{
    public Guid Id { get; set; }

	  public Guid OrdemServicoId { get; set; }
	  public OrdemServico OrdemServico { get; set; }

    public Guid PecaId { get; set; } 
    public Peca Peca { get; set; }  

    public decimal Preco { get; set; }
    public int Quantidade { get; set; }

	  public OrdemServicoPeca(Guid ordemServicoId, Guid pecaId, decimal preco, int quantidade)
    {
        OrdemServicoId = ordemServicoId;
        PecaId = pecaId;
        Preco = preco;
        Quantidade = quantidade;
    }
}

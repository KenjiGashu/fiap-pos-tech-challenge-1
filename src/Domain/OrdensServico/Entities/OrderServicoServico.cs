namespace Gashu.SistemaMecanica.Domain.OrdensServico.Entities;

public class OrdemServicoServico
{
    public Guid Id { get; set; }
    public decimal Preco { get; set; }
    public string Nome { get; set; }

    public Guid OrdemServicoId { get; set; }
    public OrdemServico OrdemServico { get; set; }

  	public Guid ServicoId { get; set; }
    public Servico Servico { get; set; } = null!;

    protected OrdemServicoServico() { }

	  public OrdemServicoServico(Guid ordemServicoId, Guid servicoId, decimal preco, string nome)
    {
        OrdemServicoId = ordemServicoId;
        ServicoId = servicoId;
        Preco = preco;
        Nome = nome;
    }
}

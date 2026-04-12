namespace Domain.OrdensServico.Entities;

public enum StatusOrdemServico
{
	Recebida,
	OrcamentoAprovado,
	EmDiagnostico,
	AguardandoAprovacao,
	EmExecucao,
	Finalizada,
	Entregue
}

public class OrdemServico
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public Guid VeiculoId { get; private set; }
    public DateTime Data { get; set; }
    public decimal Total { get; set; }
	  public StatusOrdemServico  Status { get; set; }
	

    public List<OrdemServicoServico> Servicos { get; private set; }
    public List<OrdemServicoPeca> Pecas { get; private set; }

    protected OrdemServico() { }

    public OrdemServico(Guid clienteId, Guid veiculoId)
    {
        Id = Guid.NewGuid();
        ClienteId = clienteId;
        VeiculoId = veiculoId;
        Data = DateTime.Now;

        Servicos = new List<OrdemServicoServico>();
        Pecas = new List<OrdemServicoPeca>();
    }

    public void AdicionarServico(Guid servicoId, decimal preco)
    {
        Servicos.Add(new OrdemServicoServico(servicoId, preco));
        RecalcularTotal();
    }

    public void AdicionarPeca(Guid pecaId, decimal preco, int quantidade)
    {
        Pecas.Add(new OrdemServicoPeca(pecaId, preco, quantidade));
        RecalcularTotal();
    }

    private void RecalcularTotal()
    {
        Total =
            Servicos.Sum(s => s.Preco) +
            Pecas.Sum(p => p.Preco * p.Quantidade);
    }

	public void AprovarOrcamento()
	{
		Status = StatusOrdemServico.OrcamentoAprovado;
	}

	public void IniciarDiagnostico()
	{
		Status = StatusOrdemServico.EmDiagnostico;
	}

	public void IniciarExecucao()
	{
		Status = StatusOrdemServico.EmExecucao;
	}

}

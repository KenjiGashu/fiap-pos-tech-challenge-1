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
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Guid VeiculoId { get; set; }
    public DateTime Data { get; set; }
    public decimal Total { get; set; }
	  public StatusOrdemServico  Status { get; set; }


    public List<OrdemServicoServico> OrdemServicoServicos { get; private set; } = new();
    public List<OrdemServicoPeca> OrdemServicoPecas { get; private set; } = new();

    protected OrdemServico() { }

    public OrdemServico(Guid clienteId, Guid veiculoId)
    {
        ClienteId = clienteId;
        VeiculoId = veiculoId;
        Data = DateTime.Now;

        OrdemServicoServicos = new List<OrdemServicoServico>();
        OrdemServicoPecas = new List<OrdemServicoPeca>();
    }

    public void AdicionarServico(Guid servicoId, decimal preco)
    {
			  OrdemServicoServicos.Add(new OrdemServicoServico(this.Id, servicoId, preco));
        RecalcularTotal();
    }

    public void AdicionarPeca(Guid pecaId, decimal preco, int quantidade)
    {
			if(quantidade <= 0)
            throw new Exception("Quantidade de Peca invalida.");

        OrdemServicoPecas.Add(new OrdemServicoPeca(this.Id, pecaId, preco, quantidade));
        RecalcularTotal();
    }

    private void RecalcularTotal()
    {
        Total =
					  OrdemServicoServicos.Sum(s => s.Preco) +
            OrdemServicoPecas.Sum(p => p.Preco * p.Quantidade);

        Console.WriteLine($"Novo Total: {Total}");
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

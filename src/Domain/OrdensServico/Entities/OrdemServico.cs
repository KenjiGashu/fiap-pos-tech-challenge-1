namespace Domain.OrdensServico.Entities;

public enum StatusOrdemServico
{
    Recebida,
    OrcamentoAprovado,
    EmDiagnostico,
    AguardandoAprovacao,
    AguardandoAprovacaoRevisao,
    AguardandoMecanico,
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
    public bool deveAprovarDeNovo { get; set; }


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

    public void AdicionarServico(Guid servicoId, decimal preco, string nome)
    {
        if(Status == StatusOrdemServico.EmDiagnostico)
        {
            deveAprovarDeNovo = true;
        }
            
        OrdemServicoServicos.Add(new OrdemServicoServico(this.Id, servicoId, preco, nome));
        RecalcularTotal();
    }

    public void AdicionarPeca(Guid pecaId, decimal preco, int quantidade, string nome)
    {
        if(quantidade <= 0)
            throw new Exception("Quantidade de Peca invalida.");

        if(Status == StatusOrdemServico.EmDiagnostico)
        {
            deveAprovarDeNovo = true;
        }

        OrdemServicoPecas.Add(new OrdemServicoPeca(this.Id, pecaId, nome, preco, quantidade));
        RecalcularTotal();
    }

    private void RecalcularTotal()
    {
        Total =
          OrdemServicoServicos.Sum(s => s.Preco) +
          OrdemServicoPecas.Sum(p => p.Preco * p.Quantidade);
    }

    public void AprovarOrcamento()
    {
        if(Status == StatusOrdemServico.AguardandoAprovacao)
            Status = StatusOrdemServico.OrcamentoAprovado;
        else if(Status == StatusOrdemServico.AguardandoAprovacaoRevisao)
            Status = StatusOrdemServico.AguardandoMecanico;
    }

    public void IniciarDiagnostico()
    {
        Status = StatusOrdemServico.EmDiagnostico;
    }

    public void FinalizarDiagnostico()
    {
        if(deveAprovarDeNovo)
            Status = StatusOrdemServico.AguardandoAprovacaoRevisao;
        else
            Status = StatusOrdemServico.AguardandoMecanico;
    }

    public void IniciarExecucao()
    {
        Status = StatusOrdemServico.EmExecucao;
    }

    public void FinalizarExecucao()
    {
        Status = StatusOrdemServico.Finalizada;
    }

    public void EntregarVeiculo()
    {
        Status = StatusOrdemServico.Entregue;
    }
}

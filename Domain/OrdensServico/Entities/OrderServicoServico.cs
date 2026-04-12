namespace Domain.OrdensServico.Entities;

public class OrdemServicoServico
{
    public Guid Id { get; private set; }
    public Guid ServicoId { get; private set; }
    public decimal Preco { get; private set; }

    protected OrdemServicoServico() { }

    public OrdemServicoServico(Guid servicoId, decimal preco)
    {
        Id = Guid.NewGuid();
        ServicoId = servicoId;
        Preco = preco;
    }
}

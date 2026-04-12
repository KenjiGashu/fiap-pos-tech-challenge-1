namespace Domain.OrdensServico.Entities;

public class OrdemServicoPeca
{
    public Guid Id { get; private set; }
    public Guid PecaId { get; private set; }
    public decimal Preco { get; private set; }
    public int Quantidade { get; private set; }

    protected OrdemServicoPeca() { }

    public OrdemServicoPeca(Guid pecaId, decimal preco, int quantidade)
    {
        Id = Guid.NewGuid();
        PecaId = pecaId;
        Preco = preco;
        Quantidade = quantidade;
    }
}

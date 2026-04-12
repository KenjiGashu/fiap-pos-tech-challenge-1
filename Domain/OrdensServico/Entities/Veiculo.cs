namespace Domain.OrdensServico.Entities; 

public class Veiculo
{
    public Guid Id { get; private set; }
    public string Placa { get; private set; }
    public string Marca { get; private set; }
    public string Modelo { get; private set; }
    public int Ano { get; private set; }

    protected Veiculo() { }

    public Veiculo(string placa, string marca, string modelo, int ano)
    {
        Id = Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(placa))
            throw new Exception("Placa obrigatória");

        if (ano < 1900)
            throw new Exception("Ano inválido");

        Placa = placa;
        Marca = marca;
        Modelo = modelo;
        Ano = ano;
    }

    public void Atualizar(string placa, string marca, string modelo, int ano)
    {
        Placa = placa;
        Marca = marca;
        Modelo = modelo;
        Ano = ano;
    }
}

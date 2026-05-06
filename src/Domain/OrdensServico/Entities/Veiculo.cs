namespace Gashu.SistemaMecanica.Domain.OrdensServico.Entities; 

public class Veiculo
{
    public Guid Id { get; set; }
    public string Placa { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int Ano { get; set; }

    protected Veiculo() { }

    public Veiculo(string placa, string marca, string modelo, int ano)
    {
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

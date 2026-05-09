using System.Text.RegularExpressions;

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

        if (!ValidarPlaca(placa))
            throw new Exception("Placa invalida");

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

    public bool ValidarPlaca(string placa)
    {
        bool valido = false;
        string regexPlacaNova = "^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$";
        string regexPlacaAntiga = "^[A-Z]{3}-[0-9]{4}$";

        valido = Regex.IsMatch(placa, regexPlacaNova);
        valido |= Regex.IsMatch(placa, regexPlacaAntiga);

        return valido;
    }
}

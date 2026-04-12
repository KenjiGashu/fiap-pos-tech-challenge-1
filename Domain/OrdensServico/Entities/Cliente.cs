namespace Domain.OrdensServico.Entities;
using System.Text.RegularExpressions;


public enum TipoPessoa {
	Fisica = 1,
	Juridica = 2
}

public class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }
    public string Cnpj { get; private set; }
    public TipoPessoa TipoPessoa { get; private set; }


    public Cliente(string nome, string email, string cpf, string cnpj, TipoPessoa tipoPessoa)
    {
        bool pessoaValida = tipoPessoa == TipoPessoa.Fisica ? ValidarCpf(cpf) : ValidarCnpj(cnpj);

        if (!pessoaValida)
        {
            throw new Exception(string.Format("CPF/CNPJ invalido. cpf[{0}] cnpj[{1}]", cpf, cnpj));
        }

        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Cpf = cpf;
        Cnpj = cnpj;
        TipoPessoa = tipoPessoa;
    }

    public void Atualizar(string nome, string email, string cpf, string cnpj, TipoPessoa tipoPessoa)
    {
        Nome = nome;
        Email = email;
        Cpf = cpf;
        Cnpj = cnpj;
        TipoPessoa = tipoPessoa;
    }

	public bool ValidarCpf(string cpf)
	{
    if (string.IsNullOrWhiteSpace(cpf))
        return false;

    // Remove tudo que não for número
    cpf = new string(cpf.Where(char.IsDigit).ToArray());

    if (cpf.Length != 11)
        return false;

    // Elimina CPFs inválidos conhecidos (todos iguais)
    if (new string(cpf[0], 11) == cpf)
        return false;

    int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

    string tempCpf = cpf.Substring(0, 9);

    int soma = 0;
    for (int i = 0; i < 9; i++)
        soma += (tempCpf[i] - '0') * multiplicador1[i];

    int resto = soma % 11;
    resto = resto < 2 ? 0 : 11 - resto;

    string digito = resto.ToString();
    tempCpf += digito;

    soma = 0;
    for (int i = 0; i < 10; i++)
        soma += (tempCpf[i] - '0') * multiplicador2[i];

    resto = soma % 11;
    resto = resto < 2 ? 0 : 11 - resto;

    digito += resto.ToString();

    return cpf.EndsWith(digito);
	}

    public bool ValidarCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        // Remove caracteres não numéricos
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpj.Length != 14)
            return false;

        // Elimina CNPJs inválidos conhecidos (todos iguais)
        if (new string(cnpj[0], 14) == cnpj)
            return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);

        int soma = 0;
        for (int i = 0; i < 12; i++)
            soma += (tempCnpj[i] - '0') * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();

        tempCnpj += digito;

        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += (tempCnpj[i] - '0') * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }

}


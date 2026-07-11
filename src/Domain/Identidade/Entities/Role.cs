namespace Gashu.SistemaMecanica.Domain.Identidade.Entities;

public class Role
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }

    public Role(string nome)
    {
        this.Nome = nome;
    }
}

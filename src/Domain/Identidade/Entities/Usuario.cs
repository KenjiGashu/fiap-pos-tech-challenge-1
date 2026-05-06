namespace Gashu.SistemaMecanica.Domain.Identidade.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    public List<Role> Roles { get; private set; }

    public Usuario(string email, string passwordHash)
    {
        this.Email = email;
        this.PasswordHash = passwordHash;
        this.Roles = new List<Role>();
    }
}

namespace Gashu.SistemaMecanica.Application.Identidade.Services;

public class LoginDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class UsuarioResponseDto
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required List<string> Roles { get; set; }
}

public class CriarUsuarioDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required List<string> Roles { get; set; }
}

namespace Application.Identidade.DTOs;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UsuarioResponseDto
{
		public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Roles { get; set; }
}

public class CriarUsuarioDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Roles { get; set; }
}

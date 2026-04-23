namespace Application.Identidade.DTOs;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class ObterTodosResponseDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Roles { get; set; }
}

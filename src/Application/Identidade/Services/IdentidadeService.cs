namespace Gashu.SistemaMecanica.Application.Identidade.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Gashu.SistemaMecanica.Application.Identidade.Interfaces;
using Gashu.SistemaMecanica.Domain.Identidade.Entities;
using Gashu.SistemaMecanica.Domain.Identidade.Interfaces;
using System.Linq;
using Gashu.SistemaMecanica.Application.Identidade.Services;
using System.Security.Cryptography;

public class IdentidadeService : IIdentidadeService
{
    readonly IUsuarioRepository _usuarioRepo;
    readonly IJwtService _jwtService;

    public IdentidadeService(IUsuarioRepository repo, IJwtService jwtService)
    {
        this._usuarioRepo = repo;
        this._jwtService = jwtService;
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _usuarioRepo.ObterPorEmail(email);
        if (user == null || !VerificaPassword(password, user.PasswordHash))
            throw new Exception("Credenciais inválidas");

        return _jwtService.GenerateToken(user);
    }

    public async Task CriaUsuario(string email, string password, IEnumerable<string> roles)
    {
        var hashedPassword = HashPassword(password);
        var usuario = new Usuario(email, hashedPassword);
        usuario.Roles.AddRange(roles.Select(r => new Role(r)));

        await _usuarioRepo.Adicionar(usuario);
    }

    public async Task<IEnumerable<Usuario>> ObterTodos()
    {
        var usuarios =  await _usuarioRepo.ObterTodos();
        
        var usuariosResponse = usuarios.Select(u => new UsuarioResponseDto
        {
            Id = u.Id,
            Email = u.Email,
            Password = u.PasswordHash,
            Roles = u.Roles.Select(r => r.Nome).ToList()
        });

        //return usuariosResponse;

        return usuarios;
    }

    public async Task<Usuario> ObterPorEmail(string email)
    {
        var usuario =  await _usuarioRepo.ObterPorEmail(email);

        if(usuario == null)
            throw new Exception("Usuario Invalido");

        var usuarioResponse = new UsuarioResponseDto
        {
            Id = usuario.Id,
            Email = usuario.Email,
            Password = usuario.PasswordHash,
            Roles = usuario.Roles.Select(r => r.Nome).ToList()
        };

        //return usuarioResponse;
        return usuario;
    }

    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            100000,
            HashAlgorithmName.SHA256,
            32
            );

        // junta salt + hash
        var hashBytes = new byte[48];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
        Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);

        return Convert.ToBase64String(hashBytes);
    }

    public static bool VerificaPassword(string password, string storedHash)
    {
        var hashBytes = Convert.FromBase64String(storedHash);

        var salt = new byte[16];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, 16);

        var hash = new byte[32];
        Buffer.BlockCopy(hashBytes, 16, hash, 0, 32);

        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            100000,
            HashAlgorithmName.SHA256,
            32
            );

        var hashBytesOutput = new byte[48];
        Buffer.BlockCopy(salt, 0, hashBytesOutput, 0, 16);
        Buffer.BlockCopy(hashToCompare, 0, hashBytesOutput, 16, 32);

        return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
    }
}

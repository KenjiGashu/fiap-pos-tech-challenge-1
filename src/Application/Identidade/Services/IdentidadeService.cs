namespace Application.Identidade.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Identidade.Interfaces;
using Domain.Identidade.Entities;
using Domain.Identidade.Interfaces;
using System.Linq;
using Application.Identidade.DTOs;
using System.Security.Cryptography;

public class IdentidadeService : IIdentidadeService
{
    IUsuarioRepository _usuarioRepo;
    IJwtService _jwtService;

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

    public async Task CriaUsuario(string email, string password)
    {
        var hashedPassword = HashPassword(password);
        var usuario = new Usuario(email, hashedPassword);

        await _usuarioRepo.Adicionar(usuario);
    }

    public async Task<IEnumerable<ObterTodosResponseDto>> ObterTodos()
    {
        var usuarios =  await _usuarioRepo.ObterTodos();
        var responseDto = usuarios.Select(u => new ObterTodosResponseDto
        {
            Email = u.Email,
            Password = u.PasswordHash,
            Roles = u.Roles.Select(r => r.Nome).ToList()
        });

        return responseDto;
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

    public bool VerificaPassword(string password, string storedHash)
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

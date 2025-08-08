using AppBackEnd.Data;
using AppBackEnd.DTOs;
using AppBackEnd.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AppBackEnd.Services
{
    public class AuthService : IAuthService
    {
        private readonly InMemoryDataService _dataService;
        private readonly IConfiguration _configuration;

        public AuthService(InMemoryDataService dataService, IConfiguration configuration)
        {
            _dataService = dataService;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var usuario = _dataService.GetUsuarioByEmail(loginDto.Email);

            if (usuario == null || !VerifyPassword(loginDto.Senha, usuario.Senha))
            {
                throw new UnauthorizedAccessException("Email ou senha inválidos");
            }

            var token = GenerateJwtToken(usuario);
            
            return new AuthResponseDto
            {
                Token = token,
                Usuario = new UsuarioDto
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                }
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = _dataService.GetUsuarioByEmail(registerDto.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Email já cadastrado");
            }

            var hashedPassword = HashPassword(registerDto.Senha);
            
            var usuario = new Usuario
            {
                Nome = registerDto.Nome,
                Email = registerDto.Email,
                Senha = hashedPassword,
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            };

            _dataService.AddUsuario(usuario);

            var token = GenerateJwtToken(usuario);
            
            return new AuthResponseDto
            {
                Token = token,
                Usuario = new UsuarioDto
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                }
            };
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"] ?? "DefaultSecretKeyForDevelopmentOnly");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationInMinutes"] ?? "60")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
    }
}

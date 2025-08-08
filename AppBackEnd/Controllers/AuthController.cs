using Microsoft.AspNetCore.Mvc;
using AppBackEnd.DTOs;
using AppBackEnd.Services;

namespace AppBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Realiza o login do usuário
        /// </summary>
        /// <param name="loginDto">Dados de login</param>
        /// <returns>Token JWT e informações do usuário</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="registerDto">Dados de registro</param>
        /// <returns>Token JWT e informações do usuário</returns>
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterAsync(registerDto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Endpoint de teste para verificar se a API está funcionando
        /// </summary>
        /// <returns>Mensagem de status</returns>
        [HttpGet("health")]
        public ActionResult<string> Health()
        {
            return Ok(new { message = "API de Gestão de Consultas está funcionando!", timestamp = DateTime.UtcNow });
        }
    }
}

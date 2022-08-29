using GerenciadorDeTarefas.Services;
using GerenciadorDeTarefas.Dtos;
using Microsoft.AspNetCore.Mvc;
using GerenciadorDeTarefas.Models;
using Microsoft.AspNetCore.Authorization;
using GerenciadorDeTarefas.Repository;
using GerenciadorDeTarefas.Utils;

namespace GerenciadorDeTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        // Mock user
        // private readonly string loginTestEmail = "admin@admin.com";
        // private readonly string loginTestPassword = "Admin@1234";

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, IUserRepository userRepository) : base(userRepository)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                if (loginRequest == null ||
                    string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password) ||
                    string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Error = "Dados informados incorretamente!"
                    });
                }

                var user = _userRepository.GetUserByLoginAndPassword(loginRequest.Email, MD5Utils.GenerateHashMD5(loginRequest.Password));

                if (user == null)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Error = "Usuário ou senha inválidos!"
                    });
                }

                var token = TokenService.GenerateToken(user);

                return Ok(new ResponseLoginRequestDto()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Token = token
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao realizar login.", e, loginRequest);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Error = "Ocorreu erro ao efetuar login, tente novamente!"
                });
            }
        }
    }
}
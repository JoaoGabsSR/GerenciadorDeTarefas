using GerenciadorDeTarefas.Services;
using GerenciadorDeTarefas.Dtos;
using Microsoft.AspNetCore.Mvc;
using GerenciadorDeTarefas.Models;
using Microsoft.AspNetCore.Authorization;

namespace GerenciadorDeTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        // Mocked user
        private readonly string loginTestEmail = "admin@admin.com";
        private readonly string loginTestPassword = "Admin@1234";

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
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
                    string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password) ||
                    loginRequest.Email != loginTestEmail || loginRequest.Password != loginTestPassword)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Error = "Dados informados incorretamente!"
                    });
                }

                var userTest = new User()
                {
                    Id = 1,
                    Name = "Usuário de Testes",
                    Email = loginTestEmail,
                    Password = loginTestPassword
                };

                var token = TokenService.GenerateToken(userTest);

                return Ok(new ResponseLoginRequestDto()
                {
                    Name = userTest.Name,
                    Email = userTest.Email,
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
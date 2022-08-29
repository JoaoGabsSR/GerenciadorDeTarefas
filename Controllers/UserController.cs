using System.Text.RegularExpressions;
using GerenciadorDeTarefas.Dtos;
using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Repository;
using GerenciadorDeTarefas.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository) : base(userRepository)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SaveUser([FromBody] User user)
        {
            try
            {
                // List of Errors
                var errors = new List<string>();

                // Regex
                Regex regexEmail = new Regex(@"^([\w\.\-\+\d]+)@([\w\-]+)((\.(\w){2,4})+)$");
                Regex regexPassword = new Regex(@"^[a-zA-Z0-9]+");

                if (string.IsNullOrEmpty(user.Name) || string.IsNullOrWhiteSpace(user.Name) || user.Name.Length < 2)
                {
                    errors.Add("Nome inválido");
                }

                if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Email) || !regexEmail.Match(user.Email).Success)
                {
                    errors.Add("Email inválido");
                }

                if (_userRepository.GetUserByEmail(user.Email))
                {
                    errors.Add("Email já cadastrado");
                }

                if (string.IsNullOrEmpty(user.Password) || string.IsNullOrWhiteSpace(user.Password) || user.Email.Length < 4 && !regexPassword.Match(user.Password).Success)
                {
                    errors.Add("Senha inválida");
                }

                if (errors.Count > 0)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Errors = errors
                    });
                }

                //Lower case email 
                user.Email = user.Email.ToLower();
                // Cryptographing password
                user.Password = MD5Utils.GenerateHashMD5(user.Password);

                _userRepository.Save(user);

                return Ok(new { msg = "Usuário cadastrado com sucesso." });
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao cadastrar usário.", e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Error = "Ocorreu erro ao cadastrar usário, tente novamente!"
                });
            }
        }
    }
}
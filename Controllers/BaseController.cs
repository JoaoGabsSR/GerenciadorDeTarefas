using System.Security.Claims;
using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefas.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected readonly IUserRepository _userRepository;

        public BaseController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected User ReadToken()
        {
            var idUserStr = User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(u => u.Value).FirstOrDefault();

            if (!string.IsNullOrEmpty(idUserStr))
            {
                var user = _userRepository.GetById(int.Parse(idUserStr));
                return user;
            }

            throw new UnauthorizedAccessException("Token não informado");
        }
    }
}
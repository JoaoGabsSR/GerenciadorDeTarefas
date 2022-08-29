using GerenciadorDeTarefas.Dtos;
using GerenciadorDeTarefas.Enums;
using GerenciadorDeTarefas.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : BaseController
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;

        public TaskController(ILogger<TaskController> logger, IUserRepository userRepository, ITaskRepository taskRepository) : base(userRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        // Erro de ambiguidade entre o GerenciadorDeTarefas.Models.Task e System.Threading.Tasks.Task
        // Não alterar referência de pasta
        [HttpPost]
        public IActionResult CreateTask([FromBody] GerenciadorDeTarefas.Models.Task task)
        {
            try
            {
                var user = ReadToken();
                var errors = new List<string>();

                if (task == null || user == null)
                {
                    errors.Add("Favor informar a tarefa ou usuário");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(task.TaskName) || string.IsNullOrEmpty(task.TaskName) || task.TaskName.Count() < 4)
                    {
                        errors.Add("Favor informar um nome válido");
                    }

                    if (task.DateToFinishing == DateTime.MinValue || task.DateToFinishing.Date < DateTime.Now.Date)
                    {
                        errors.Add("Favor informar uma data de previsão válida e que não seja menor que hoje");
                    }
                }

                if (errors.Count > 0)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Errors = errors
                    });
                }

                task.IdUser = user.Id;
                task.DateFinished = null;
                _taskRepository.CreateNewTask(task);

                return Ok(new { msg = "Tarefa criada com sucesso." });
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao criar nova tarefa.", e, task);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Error = "Ocorreu erro ao criar nova tarefa, tente novamente!"
                });
            }
        }

        [HttpDelete("{idTask}")]
        public IActionResult DeleteTask(int idTask)
        {
            try
            {
                var user = ReadToken();
                var task = _taskRepository.GetTaskById(idTask);

                if (user == null || idTask <= 0)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Error = "Usuário ou tarefa inválida"
                    });
                }

                if (task == null || task.IdUser != user.Id)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Error = "Tarefa não encontrada"
                    });
                }

                _taskRepository.RemoveTask(task);

                return Ok(new { msg = "Tarefa deletada com sucesso." });
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao deletar tarefa.", e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Error = "Ocorreu erro ao deletar tarefa, tente novamente!"
                });
            }
        }

        [HttpPut("{idTask}")]
        public IActionResult UpdateTask([FromBody] Models.Task model, int idTask)
        {
            try
            {
                var user = ReadToken();

                if (user == null || idTask <= 0)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Error = "Usuário ou tarefa inválida"
                    });
                }


                var task = _taskRepository.GetTaskById(idTask);
                var errors = new List<string>();

                if (task == null || task.IdUser != user.Id)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Error = "Tarefa não encontrada"
                    });
                }

                if (model == null)
                {
                    errors.Add("Favor informar a tarefa ou usuário");
                }
                else if (!string.IsNullOrWhiteSpace(model.TaskName) && !string.IsNullOrEmpty(model.TaskName) && model.TaskName.Count() < 4)
                {
                    errors.Add("Favor informar um nome válido");
                }

                if (errors.Count > 0)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Errors = errors
                    });
                }

                if (!string.IsNullOrWhiteSpace(model.TaskName) && !string.IsNullOrEmpty(model.TaskName))
                {
                    task.TaskName = model.TaskName;
                }

                if (model.DateToFinishing != DateTime.MinValue)
                {
                    task.DateToFinishing = model.DateToFinishing;
                }

                if (model.DateFinished != null && model.DateFinished != DateTime.MinValue)
                {
                    task.DateFinished = model.DateFinished;
                }

                _taskRepository.UpdateTask(task);

                return Ok(new { msg = "Tarefa atualizada com sucesso." });
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao atualizar tarefa.", e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Error = "Ocorreu erro ao atualizar tarefa, tente novamente!"
                });
            }
        }

        [HttpGet]
        public IActionResult GetTasks(DateTime? periodFrom, DateTime? periodAt, StatusTaskEmum status)
        {
            try
            {
                var user = ReadToken();

                if (user == null)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Error = "Usuário não encontrado"
                    });
                }

                var result = _taskRepository.ListTasks(user.Id, periodFrom, periodAt, status);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao listar tarefas.", e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Error = "Ocorreu erro ao listar tarefas, tente novamente!"
                });
            }
        }
    }
}
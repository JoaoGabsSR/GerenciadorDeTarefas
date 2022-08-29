using GerenciadorDeTarefas.Enums;

namespace GerenciadorDeTarefas.Repository
{
    public interface ITaskRepository
    {
        // Erro de ambiguidade entre o GerenciadorDeTarefas.Models.Task e System.Threading.Tasks.Task
        // Não alterar referência de pasta
        void CreateNewTask(Models.Task task);
        Models.Task GetTaskById(int id);
        void RemoveTask(Models.Task task);
        void UpdateTask(Models.Task task);
        List<Models.Task> ListTasks(int userId, DateTime? periodFrom, DateTime? periodAt, StatusTaskEmum status);
    }
}
using GerenciadorDeTarefas.Data;
using GerenciadorDeTarefas.Enums;

namespace GerenciadorDeTarefas.Repository.Impl
{
    public class TaskRepositoryImpl : ITaskRepository
    {
        private readonly GerenciadorDeTarefasContext _context;
        public TaskRepositoryImpl(GerenciadorDeTarefasContext context)
        {
            _context = context;
        }

        // Erro de ambiguidade entre o GerenciadorDeTarefas.Models.Task e System.Threading.Tasks.Task
        // Não alterar referência de pasta
        public void CreateNewTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public Models.Task GetTaskById(int id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public List<Models.Task> ListTasks(int userId, DateTime? periodFrom, DateTime? periodAt, StatusTaskEmum status)
        {
            return _context.Tasks.Where(t => t.IdUser == userId
                    && (periodFrom == null || periodFrom == DateTime.MinValue || t.DateToFinishing > ((DateTime)periodFrom).Date)
                    && (periodAt == null || periodAt == DateTime.MinValue || t.DateToFinishing > ((DateTime)periodAt).Date)
                    && (status == StatusTaskEmum.All || (status == StatusTaskEmum.Active && t.DateFinished == null)
                                                    || (status == StatusTaskEmum.Complete && t.DateFinished != null))
                    ).ToList();
        }

        public void RemoveTask(Models.Task task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Models.Task task)
        {
            _context.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }
    }
}
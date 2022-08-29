using GerenciadorDeTarefas.Data;
using GerenciadorDeTarefas.Models;

namespace GerenciadorDeTarefas.Repository.Impl
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly GerenciadorDeTarefasContext _context;
        public UserRepositoryImpl(GerenciadorDeTarefasContext context)
        {
            _context = context;
        }

        public void Save(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool GetUserByEmail(string email)
        {
            return _context.Users.Any(user => user.Email.ToLower() == email.ToLower());
        }

        public User GetUserByLoginAndPassword(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower() && u.Password == password);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
using GerenciadorDeTarefas.Models;

namespace GerenciadorDeTarefas.Repository
{
    public interface IUserRepository
    {
        void Save(User user);
        bool GetUserByEmail(string email);
        User GetUserByLoginAndPassword(string email, string password);
        User GetById(int id);
    }
}
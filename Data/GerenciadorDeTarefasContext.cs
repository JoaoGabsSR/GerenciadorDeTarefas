using GerenciadorDeTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.Data
{
    public class GerenciadorDeTarefasContext : DbContext
    {
        //"Server=localhost:3306;Database=gerenciadordetarefas;Uid=root;Pwd=04gabriel30;"
        public GerenciadorDeTarefasContext(DbContextOptions<GerenciadorDeTarefasContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
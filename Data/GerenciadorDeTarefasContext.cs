using GerenciadorDeTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.Data
{
    public class GerenciadorDeTarefasContext : DbContext
    {
        public GerenciadorDeTarefasContext(DbContextOptions<GerenciadorDeTarefasContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
    }
}
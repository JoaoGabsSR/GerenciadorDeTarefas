using System.Text.Json.Serialization;

namespace GerenciadorDeTarefas.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public virtual ICollection<Task>? Tasks { get; private set; }
    }
}
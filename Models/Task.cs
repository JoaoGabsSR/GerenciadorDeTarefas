using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GerenciadorDeTarefas.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string TaskName { get; set; }
        public DateTime DateToFinishing { get; set; }
        public DateTime? DateFinished { get; set; }

        [JsonIgnore]
        [ForeignKey("IdUser")]
        public virtual User? user { get; private set; }
    }
}
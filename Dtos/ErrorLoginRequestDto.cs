namespace GerenciadorDeTarefas.Dtos
{
    public class ResponseErrorDto
    {
        public int Status { get; set; }
        public string Error { get; set; }
        public List<string> Errors { get; set; }
    }
}
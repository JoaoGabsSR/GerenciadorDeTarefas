namespace GerenciadorDeTarefas
{
    public class JwtKey
    {
        // Random key generator
        // public static string KeyGenerator()
        // {
        //     string key = Guid.NewGuid()
        //                     .ToString("d")
        //                     .Substring(1, 24);
        //     return key;
        // }

        public static string SecretKey = "MinhaChaveUltraSecretaQueEuNaoPossoPassaPraNinguem";
    }
}
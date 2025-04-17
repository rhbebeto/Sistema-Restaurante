namespace SistemaRestaurante.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; } // Pode criptografar depois
        public string Setor { get; set; } // "Cozinha", "Copa", "Admin"
    }
}

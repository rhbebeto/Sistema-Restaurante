namespace SistemaRestaurante.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; } // "Prato" ou "Bebida"
        public decimal Preco { get; set; }
    }
}

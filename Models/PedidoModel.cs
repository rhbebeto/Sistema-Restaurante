namespace SistemaRestaurante.Models
{ 
    public class PedidoModel
    {
        public int Id { get; set; }
        public string NomeSolicitante { get; set; }
        public int Mesa { get; set; }
        public string Status { get; set; } // "Em preparo", "Pronto", "Entregue"
        public DateTime DataHora { get; set; }

        public List<PedidoItemModel> Itens { get; set; } = new List<PedidoItemModel>();
    }
}
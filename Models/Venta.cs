namespace ExperimentoAPI.Models
{
    public class Venta
    {
        public int id { get; set; }
        public int usuarioId {  get; set; }
        public DateTime fecha { get; set; }
        public decimal total { get; set; }
        public IEnumerable<DetalleVenta> detallesVenta { get; set; }

    }
}

namespace ExperimentoAPI.Models
{
    public class DetalleVenta
    {
        public int id { get; set; }
        public int ventaId { get; set; }
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal subTotal { get; set; }


    }
}

namespace ExperimentoAPI.Models
{
    public class CarritoDetalle
    {
        public int Id { get; set; }
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }
        public int ProductoId {get;set;}
        public Producto2 Producto { get; set; }
        public int cantidad { get; set;}
    }
}

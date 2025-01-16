namespace ExperimentoAPI.Models
{
    public class Producto2
    {
       
            public int Id { get; set; }
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
            public int Stock { get; set; }
        public int? idCategoria { get; set; }
        public Categoria categoria { get; set; }
        
    }
}

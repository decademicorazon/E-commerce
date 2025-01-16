namespace ExperimentoAPI.Models
{
    public class Categoria
    {


       
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }

         
            public ICollection<Producto2> productos { get; set; }
        

    }
}

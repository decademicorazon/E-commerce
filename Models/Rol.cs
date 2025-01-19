namespace ExperimentoAPI.Models
{
    public class Rol
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public ICollection<Consumidor> Consumidores { get; set; }   
    }
}

using System.Text.Json.Serialization;

namespace ExperimentoAPI.Models
{
    public class Consumidor
    {
     
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public bool EstaActivo { get; set; }


        [JsonIgnore]
        public ICollection<Carrito> Carritos { get; set; }
        
    }
}

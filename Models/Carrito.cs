using System.Text.Json.Serialization;

namespace ExperimentoAPI.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        [JsonIgnore]
        public Consumidor Usuario { get; set; }

        
        public ICollection<CarritoDetalle> Detalles { get; set; }
    }
}

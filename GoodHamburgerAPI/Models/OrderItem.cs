using System.Text.Json.Serialization;

namespace GoodHamburgerAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; } // Mantemos esta navegação

        public int OrderId { get; set; }

        [JsonIgnore] // Adicione este atributo
        public Order Order { get; set; }
    }
}
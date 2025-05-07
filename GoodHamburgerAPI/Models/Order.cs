using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GoodHamburgerAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        [JsonIgnore] // Adicione este atributo
        public List<OrderItem> Items { get; set; } = new();
    }
}
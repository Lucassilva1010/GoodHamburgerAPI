using Microsoft.AspNetCore.Mvc;
using GoodHamburgerAPI.Data;
using GoodHamburgerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburgerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/orders (Lista todos os pedidos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToListAsync();
        }

        // POST: api/orders (Cria um novo pedido)
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder([FromBody] OrderRequest request)
        {
            // 1. Validação básica
            if (request?.Items == null || request.Items.Count == 0)
                return BadRequest("O pedido deve conter itens.");

            // 2. Criar o objeto Order (aqui declaramos a variável 'order')
            var order = new Order
            {
                Items = request.Items.Select(item => new OrderItem { ProductId = item.ProductId }).ToList(),
                OrderDate = DateTime.Now
            };

            // 3. Verificar se há mais de um sanduíche
            var sandwichCount = order.Items.Count(i =>
                _context.Products.Find(i.ProductId)?.Category == "Sandwich");

            if (sandwichCount > 1)
                return BadRequest("Apenas um sanduíche por pedido é permitido.");

            // 4. Verificar itens duplicados
            var duplicateItems = order.Items
                .GroupBy(i => i.ProductId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateItems.Any())
            {
                var productNames = duplicateItems
                    .Select(id => _context.Products.Find(id)?.Name)
                    .Where(name => name != null);

                return BadRequest($"Itens duplicados não são permitidos: {string.Join(", ", productNames)}");
            }

            // 5. Calcular o total com descontos
            var items = order.Items
                .Select(i => _context.Products.Find(i.ProductId))
                .Where(p => p != null)
                .ToList();

            decimal total = items.Sum(p => p.Price);
            var sandwich = items.FirstOrDefault(p => p.Category == "Sandwich");
            var hasFries = items.Any(p => p.Name == "Fries");
            var hasDrink = items.Any(p => p.Name == "Soft drink");

            if (sandwich != null)
            {
                if (hasFries && hasDrink)
                    total *= 0.8m; // 20% de desconto
                else if (hasDrink)
                    total *= 0.85m; // 15% de desconto
                else if (hasFries)
                    total *= 0.9m; // 10% de desconto
            }

            order.TotalAmount = total;

            // 6. Salvar no banco de dados
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // 7. Mapear para DTO antes de retornar
            var orderDto = new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    ProductPrice = i.Product.Price
                }).ToList()
            };

            return CreatedAtAction("GetOrders", new { id = order.Id }, orderDto);
        }

        public class OrderRequest
        {
            public List<OrderItemRequest> Items { get; set; }
        }

        public class OrderItemRequest
        {
            public int ProductId { get; set; }
        }

        // DELETE: api/orders/1 (Remove um pedido)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
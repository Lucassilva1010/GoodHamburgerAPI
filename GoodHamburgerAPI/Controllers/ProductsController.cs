using Microsoft.AspNetCore.Mvc;
using GoodHamburgerAPI.Data;
using GoodHamburgerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburgerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products (Lista todos os produtos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/products/sandwiches (Lista só sanduíches)
        [HttpGet("sandwiches")]
        public async Task<ActionResult<IEnumerable<Product>>> GetSandwiches()
        {
            return await _context.Products
                .Where(p => p.Category == "Sandwich")
                .ToListAsync();
        }

        // GET: api/products/extras (Lista só extras)
        [HttpGet("extras")]
        public async Task<ActionResult<IEnumerable<Product>>> GetExtras()
        {
            return await _context.Products
                .Where(p => p.Category == "Extra")
                .ToListAsync();
        }
    }
}
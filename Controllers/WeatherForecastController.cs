using ExperimentoAPI.Data;
using ExperimentoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExperimentoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly InventoryDbContext _context;
        
        public ProductsController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.productos.ToListAsync();

        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Producto>> GetProducto(int id)
        {

            var prodcutoAbuscar = await _context.productos.FindAsync(id);
            if (prodcutoAbuscar == null)
            {
                return NotFound();
            }
            return prodcutoAbuscar;

        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> CrearProducto([FromBody]Producto produ)
        {
            if (produ == null)
            {
                BadRequest("El producto no puede ser nulo");

            }
            if (string.IsNullOrEmpty(produ.nombre) || produ.precio <= 0 || produ.stock < 0)
            {
                return BadRequest("Los datos del producto no son válidos.");
            }
            try
            {
                _context.productos.Add(produ);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProducto), new { id = produ.id }, produ);
            }
            catch (Exception ex) { return StatusCode(500, $"Error interno: {ex.Message}"); }

        }


        [HttpPut("{id}")]

        public async Task<ActionResult>ModificarProducto(int id, Producto produ)
        {
            if (id != produ.id)
            {
                return BadRequest("Producto no encontrado");
            }
            _context.Entry(produ).State= EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) {
            
            if(!_context.productos.Any(p => p.id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
               
            }
            return NoContent();
            

            
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> EliminarProducto(int id)
        {
            var produAbuscar = await _context.productos.FindAsync(id);
            if (produAbuscar == null)
            {
                return NotFound();
            }
            _context.productos.Remove(produAbuscar);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

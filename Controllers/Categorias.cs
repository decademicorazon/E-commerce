using ExperimentoAPI.Data;
using ExperimentoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExperimentoAPI.Controllers
{
    public class Categorias : Controller

    {
        private readonly InventoryDbContext _context;
        public Categorias(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet("VerCategorias")]

        public async Task<IActionResult> VerCategorias()
        {
            return Ok(await _context.categorias.ToListAsync());
        }
        [HttpGet("VerCategoriaId")]

        public async Task<IActionResult> VerCategoriaId(int id)
        {
            var categoria = await _context.categorias.FirstOrDefaultAsync(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound(new { message = "Categoria no encontrada" });
            }
            return Ok(categoria);
        }


        [HttpPost("AgregarCategoria")]
        [Authorize]
        public async Task<IActionResult> AgregarCategoria(Categoria categoria)
        {
            var rol = User.Claims.FirstOrDefault(c => c.Type == "rol")?.Value;
            if(rol != "admin")
            {
                return Unauthorized(new { message = "No tienes permisos para agregar una categoria" });
            }
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                return BadRequest(new { message = "El nombre de la categoria no puede ser nulo" });
            }
            var categoriaExistente = await _context.categorias.FirstOrDefaultAsync(c => c.Nombre.ToLower().Trim() == categoria.Nombre.ToLower().Trim());
            if (categoriaExistente != null)
            {
                return Conflict(new { message = "La categoria ya existe" });
            }
            _context.categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Categoria agregada con éxito" });


        }
        [HttpPut("ActualizarCategoria")]
        public async Task<IActionResult> ModificarCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest(new { message = "El id de la categoria no coincide" });
            }

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Categoria actualizada con éxito" });

        }

        [HttpDelete("EliminarCategoria")]
        public async Task<IActionResult> EliminarCategoria (int id)
        {
            var categoriaExistente = await _context.categorias.FirstOrDefaultAsync(c => c.Id == id);
            if (categoriaExistente == null)
            {
                return NotFound(new { message = "Categoria no encontrada" });
            }

            _context.categorias.Remove(categoriaExistente);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Categoria eliminada con éxito" });

        }






    }
}

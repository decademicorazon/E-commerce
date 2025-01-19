using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExperimentoAPI.Data;
using ExperimentoAPI.Models;

namespace ExperimentoAPI.Controllers
{
    public class Producto2Controller : Controller
    {
        private readonly InventoryDbContext _context;

        public Producto2Controller(InventoryDbContext context)
        {
            _context = context;
        }



        [HttpGet("VerProductos")]

        public async Task<IActionResult> VerProductos()
        {
            return Ok(await _context.productos2.ToListAsync());
        }

        [HttpGet("BuscarPorRangoPrecio")]

        public async Task<ActionResult<IEnumerable<Producto2>>> BuscarPorRangoPrecio(decimal min,decimal max)
        {
            var productos =  await _context.productos2.Where(p=>p.Precio>=min && p.Precio<=max).ToListAsync();
            return Ok(productos);
        }


        [HttpGet("BuscarPorCategoriaId/{id}")]

        public async Task<ActionResult<IEnumerable<Producto2>>> BuscarPorCategoriaId(int categoriaId)
        {

            var productos = await _context.productos2.Where(p=>p.idCategoria==categoriaId).ToListAsync();

            return Ok(productos);
        }
    }
} 
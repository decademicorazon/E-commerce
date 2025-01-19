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
    public class ConsumidorsController : Controller
    {
        private readonly InventoryDbContext _context;
        private readonly JwtService _jwtService;

        public ConsumidorsController(InventoryDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpGet("VerUsuarios")]
        public async Task<ActionResult<IEnumerable<Consumidor>>> GetConsumidor()
        {
            return await _context.Consumidor.ToListAsync();

        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegistrarUser([FromBody] Consumidor consumidor)
        {
            var usuarioExistente = await _context.Consumidor.FirstOrDefaultAsync(c => c.Username == consumidor.Username);
            if (usuarioExistente != null)
            {

                return Conflict(new { message = "El nombre de usuario ya esta en uso" });
            }

            _context.Consumidor.Add(consumidor);
            await _context.SaveChangesAsync();


            var nuevoCarrito = new Carrito
            {
                UsuarioId = consumidor.Id
            };
            _context.carritos.Add(nuevoCarrito);
            await _context.SaveChangesAsync();

           
            return Ok(new { message = "Usuario registrado con éxito."});
        }


        [HttpPost("Login")]

        public async Task<IActionResult> Login(string usuario,string contraseña)
        {
            var consumidor = await _context.Consumidor.FirstOrDefaultAsync(c=>c.Username == usuario);
            if (consumidor==null || consumidor.Password !=contraseña )
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }
            var token = _jwtService.GenerarToken(consumidor);
            return Ok(new { message = "Sesion exitosa", token });
        }
    }
}

      

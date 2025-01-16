using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExperimentoAPI.Data;
using ExperimentoAPI.Models;
using ExperimentoAPI.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace ExperimentoAPI.Controllers
{
    public class CarritoesController : Controller
    {
        private readonly InventoryDbContext _context;

        public CarritoesController(InventoryDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet("VerCarrito")]

        public async Task<IActionResult> VerCarrito(string nombreUsuario)
        {

            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                return BadRequest(new { message = "El nombre de usuario no puede ser nulo" });
            }
            var usuario = await _context.Consumidor.FirstOrDefaultAsync(c => c.Username.ToLower().Trim() == nombreUsuario.ToLower().Trim());

            if (usuario == null)
            {
                return BadRequest(new { message = "Usuario no encontrado" });
            }

            var carrito = await _context.carritos.Include(c => c.Detalles).ThenInclude(d => d.Producto).FirstOrDefaultAsync(c => c.UsuarioId == usuario.Id);
            if (carrito == null)
            {
                return BadRequest(new { message = "Carrito no asociado al usuario" });
            }


            var detalles = carrito.Detalles.Select(d => new
            {
                d.ProductoId,
                d.Producto.Nombre,
                d.Producto.Precio,
                d.cantidad,

            });

            return Ok(detalles);
        }


        [HttpPost("AgregarProducto")]

        public async Task<IActionResult> AgregarProdu([FromBody] AgregarProduRequest request)
        {
            if (request.Cantidad < 0)
            {
                return BadRequest(new { message = "La cantidad debe ser mayor o igual a 0" });
            }

            if (string.IsNullOrWhiteSpace(request.NombreUsuario))
            {
                return BadRequest(new { message = "El nombre de usuario no puede estar vacío" });
            }

            var usuario = await _context.Consumidor
                .FirstOrDefaultAsync(c => c.Username.ToLower() == request.NombreUsuario.ToLower());

            if (usuario == null)
            {
                return BadRequest(new { message = "No existe ese usuario" });
            }

            var carrito = await _context.carritos
                .Include(c => c.Detalles)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuario.Id);

            if (carrito == null)
            {
                return BadRequest(new { message = "No se encontró ningún carrito" });
            }

            var producto = await _context.productos2
                .FirstOrDefaultAsync(p => p.Id == request.ProduId);

            if (producto == null)
            {
                return BadRequest(new { message = "Producto no encontrado" });
            }

            var detalleExistente = carrito.Detalles
                .FirstOrDefault(d => d.ProductoId == request.ProduId);

            if (detalleExistente != null)
            {
                detalleExistente.cantidad += request.Cantidad; // Suma la cantidad
            }
            else
            {
                carrito.Detalles.Add(new CarritoDetalle
                {
                    ProductoId = request.ProduId,
                    cantidad = request.Cantidad
                });
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Se agregó correctamente el producto" });
        }


        [HttpDelete("EliminarProducto")]

        public async Task<IActionResult> EliminarProdu(string nombreUser, int idProdu)
        {
            if (string.IsNullOrEmpty(nombreUser))
            {
                return BadRequest(new { message = "No debe dejar el nombre en blanco" });

            }

            var usuario = await _context.Consumidor.FirstOrDefaultAsync(c => c.Username.ToLower() == nombreUser.ToLower());
            if (usuario == null) { return BadRequest(new { message = "Usuario no encontrado" }); }
            var carrito = await _context.carritos.Include(c => c.Detalles).FirstOrDefaultAsync(c => c.UsuarioId == usuario.Id);

            if (carrito == null)
            {
                return BadRequest(new { message = "No se encontró ningún carrito" });
            }
            var detalles = await _context.CarritoDetalles.FirstOrDefaultAsync(d => d.ProductoId == idProdu);
            if (detalles == null) { return BadRequest(new { message = "Producto no encontrado" }); }

            carrito.Detalles.Remove(detalles);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Producto eliminado con exito" });
        }


        [HttpPut("Modificar-Cantidad")]

        public async Task<IActionResult> ModificarCantidad(string nombreUser, int idProdu, int nuevCantidad)
        {

            if (nuevCantidad < 0)
            {
                return BadRequest(new {message="La nueva cantidad debe ser mayor a 0"});
            }
            var usuario = await _context.Consumidor.FirstOrDefaultAsync(c => c.Username.ToLower() == nombreUser.ToLower());
            if (usuario == null) { return BadRequest(new { message = "Usuario no encontrado" }); }


            var carrito = await _context.carritos.Include(c => c.Detalles).ThenInclude(d => d.Producto).FirstOrDefaultAsync(c => c.UsuarioId == usuario.Id);
            if (carrito == null) { return BadRequest(new { message = "Carrito no encontrado" }); }
            var detalles = carrito.Detalles.FirstOrDefault(d => d.ProductoId == idProdu);
            if (detalles == null) { return BadRequest(new { message = "Producto no encontrado " }); }
            detalles.cantidad = nuevCantidad;
            await _context.SaveChangesAsync();
            return Ok(new {message="Cantidad modificada correctamente"});
        }


        [HttpPost("Checkout")]

        public async Task<IActionResult> Checkout(string nombreUsuario)
        {
            var usuario = await _context.Consumidor.FirstOrDefaultAsync(c => c.Username.ToLower() == nombreUsuario.ToLower());
            if (usuario == null) { return BadRequest(new { message = "Usuario no encontrado" }); }
            var carrito = await _context.carritos.Include(c => c.Detalles).ThenInclude(d => d.Producto).FirstOrDefaultAsync(c => c.UsuarioId == usuario.Id);
            if (carrito == null || !carrito.Detalles.Any()) { return BadRequest(new { message = "Carrito no encontrado" }); }
            var ahora = DateTime.UtcNow;
            var venta = new Venta
            {
            usuarioId = usuario.Id,
            fecha = ahora,
                
             detallesVenta = carrito.Detalles.Select(d => new DetalleVenta
                {
                    idProducto = d.Id,
                    nombreProducto = d.Producto.Nombre,
                    cantidad = d.cantidad,
                    precioUnitario = d.Producto.Precio,
                    subTotal = d.cantidad * d.Producto.Precio


                }).ToList(),
                total = carrito.Detalles.Sum(d => d.cantidad * d.Producto.Precio)

            };
            _context.ventas.Add(venta);
            _context.CarritoDetalles.RemoveRange(carrito.Detalles);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Venta realizada con exito", venta });
        }
    }
}

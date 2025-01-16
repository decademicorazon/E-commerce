using ExperimentoAPI.Data;
using ExperimentoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using System.Text;

namespace ExperimentoAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly InventoryDbContext _context;
        private readonly IConfiguration _configuration;

        public ValuesController(InventoryDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login) 
        {
        var usuario = await _context.usuarios.FirstOrDefaultAsync(u=>u.usuario==login.usuario);
            if(usuario == null || usuario.contraseña!=login.contraseña)
            {
                return Unauthorized("Usuario o contraseña incorrectos");

            }
            var consumidor = await _context.Consumidor.FirstOrDefaultAsync(c => c.Username == login.usuario);

            var token = GenerarToken(consumidor);
            return Ok(new {token});

        }

        private string GenerarToken(Consumidor consumidor)
        {
            var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, consumidor.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public class LoginRequest
        {
            public string usuario { get; set; } = string.Empty;
            public string contraseña { get; set; } = string.Empty;

        }


    } 
}
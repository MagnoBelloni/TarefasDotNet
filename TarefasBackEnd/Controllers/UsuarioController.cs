using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TarefasBackEnd.Model;
using TarefasBackEnd.Repositories;
using TarefasBackEnd.ViewModel;

namespace TarefasBackEnd.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginViewModel model, [FromServices] IUsuarioRepository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var usuario = repository.Read(model.Email, model.Senha);

            if(usuario == null)
            {
                return Unauthorized();
            }

            usuario.Senha = "";

            return Ok(new {
                usuario,
                token = GenerateToken(usuario)
            });
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] Usuario model, [FromServices] IUsuarioRepository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            repository.Create(model);

            return Ok();
        }

        private string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("S[A%xzXDPr[MnQqYaqUPDaso}lgwB+");

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

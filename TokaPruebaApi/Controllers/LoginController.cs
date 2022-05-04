using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TokaInfrestructura.Halper;
using TokaNegocio.Interface;
using TokaPruebaApi.Models;

namespace TokaPruebaApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/login")]
    public class LoginController : Controller
    {
        // GET: LoginController
        private readonly IConfiguration _config;
        private readonly IUsuarioService _usuario;
        public LoginController(IConfiguration config, IUsuarioService usuario)
        {
            _config = config;
            _usuario = usuario;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(LoginViewModel Login)
        {
            var usuario = await _usuario.GetUserByEmail(Login.Correo);
            if (usuario.Success == false)
            {
                return NotFound(usuario);
            }
            if (HashHelper.CheckHash(Login.Clave, usuario.Value.Clave, usuario.Value.Sal))
            {
                var secretKey = _config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Value.Correo));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                string bearer_token = tokenHandler.WriteToken(createdToken);
                return Ok(bearer_token);
            }
            else
            {
                return Forbid();
            }
        }
        [AllowAnonymous]
        [HttpPost("AuthToken")]
        public async Task<IActionResult> AuthToken([FromHeader(Name = "X-Token")] string Token)
        {
            if (string.IsNullOrWhiteSpace(Token))
            {
                return BadRequest();
            }

            var usuario = await _usuario.GetUserByToken(Token);
            if (usuario.Success == false)
            {
                return Forbid();
            }

            var secretKey = _config.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Value.Correo));
            claims.AddClaim(new Claim("X-Token", Token));
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            string bearer_token = tokenHandler.WriteToken(createdToken);
            return Ok(bearer_token);
        }
    }
}

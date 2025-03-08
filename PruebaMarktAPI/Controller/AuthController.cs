using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PruebaMarktAPI.Data;
using PruebaMarktAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaMarktAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly pruebamarktuserContext _userContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public AuthController(pruebamarktuserContext userContext, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userContext = userContext;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] Auth auth)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var user= await _userManager.FindByEmailAsync(auth.Email);
                if (user == null)
                {
                    return NotFound();
                }
                //si no es null
                if( await _userManager.CheckPasswordAsync(user, auth.Password))
                {
                    var tokenazo = await CreateToken(user);
                    return Ok(tokenazo);
                }

                return NotFound("Contraseña Errónea");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Auth auth)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Introduzca los valores requeridos");
            }
            IdentityUser nuevoUser = new IdentityUser { Email = auth.Email, UserName = auth.Email };
            await _userManager.CreateAsync(nuevoUser, auth.Password);
            await _userManager.AddToRoleAsync(nuevoUser, "Basic");
            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> CreateToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim("Role", role));
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

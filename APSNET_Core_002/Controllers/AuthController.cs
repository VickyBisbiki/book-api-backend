using APSNET_Core_002.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APSNET_Core_002.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (login == null)
                return BadRequest("Login data is required");

            if (login.Email == "admin@test.com" && login.Password == "password")
            {
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, login.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkey1234567890!@#$%^&*()*&^*&%^%&^%S"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "ASPNET_Core_002",
                    audience: "ASPNET_Core_002",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized();
        }
    }
}

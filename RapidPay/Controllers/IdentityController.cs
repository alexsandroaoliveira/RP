using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RapidPay.Modules;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace RapidPay.Controllers
{

    // Only for test porpose - This is bad a design for production
    [ApiController]
    public class IdentityController : Controller
    {
        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] TokenRequest request)
        {
            if (request.UserId != "test")
                return new UnauthorizedResult();

            ClaimsIdentity identity = new(
                new GenericIdentity(request.UserId!, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, request.UserId!)
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(600);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "ExemploIssuer",
                Audience = "ExemploAudience",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("StoreAndLoadSecurely")),
                    SecurityAlgorithms.HmacSha256
                ),
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            return Ok(token);
        }

    }

}

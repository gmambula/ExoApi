using ExoApi.Interface;
using ExoApi.Models;
using ExoApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExoApi.Controllers
{
    [Produces("aplication/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuario _iusuario;
        public LoginController(IUsuario iusuario) 
        {
            _iusuario = iusuario;
        
        }

        [HttpPost]
        public IActionResult login(LoginViewModel login)
        {
            try
            {
                Usuarios usuarioBuscado = _iusuario.Login(login.Email, login.Senha);
                if (usuarioBuscado == null)
                {
                    return Unauthorized(new { msg = "Email e/ou Senha invalidos, tente novamente" });
                }
                // define os dados fornecido no token payload
                var minhasClaims = new []
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.Tipo)
                };

                //define chave de acesso ao token
                var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chave-autentificacao"));
                //define creednciais do token
                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
                // gera o token
                var meuToken = new JwtSecurityToken(
                    issuer: "EXOAPI",
                    audience: "EXOAPI",
                    claims: minhasClaims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credenciais
                    );
                return Ok(
                        new { token = new JwtSecurityTokenHandler().WriteToken(meuToken)}
                    );

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }    
        
        }
    }
}

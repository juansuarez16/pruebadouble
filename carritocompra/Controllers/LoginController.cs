using carritocompra.Entity.Entities.Users;
using carritocompras.Application.Services;
using carritocompras.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace carritocompra.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _usersService;

        public LoginController(IUserService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Users user)
        {
            //Verificar si el usuario existe
            var existingUser = await _usersService.GetUserByUsername(user.NombreUsuario);

            if (existingUser != null)
            {
                // Verificar si las credenciales son correctas
                if (await _usersService.VerifyPassword(existingUser, user.Contraseña))
                {
                    return Ok("Inicio de sesión exitoso");
                }
                else
                {
                    // Contraseña inválida
                    return Unauthorized("Credenciales inválidas");
                }
            }

            // Usuario no encontrado
            return NotFound("El usuario no existe");
        }
    }
}

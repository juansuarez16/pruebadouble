using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using carritocompra.Entity.Entities.Users;
using carritocompras.Application.Services.Interfaces;
using carritocompra.Entity.Entities;

namespace carritocompra.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPersonaRepository _personaService;

        public UsersController(IUserService userService, IPersonaRepository personaService)
        {
            _userService = userService;
            _personaService = personaService;
        }

        // Create (Registrar usuario)
        [HttpPost("persona")]
        public async Task<IActionResult> CreatePersona(Persona persona)
        {
            try
            {
                var createdPersona = await _personaService.CreatePersona(persona);

                if (createdPersona==null)
                {
                    return NotFound("El usuario ya existe");
                }
                return Ok(createdPersona);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al registrar persona: {ex.Message}");
            }
        }
        // Create (Registrar usuario)
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser(Users user)
        {
            try
            {
                await _userService.CreateUser(user);
                return Ok("Usuario registrado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al registrar usuario: {ex.Message}");
            }
        }

        // Read (Obtener usuarios)
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener usuarios: {ex.Message}");
            }
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {
                var user = await _userService.GetUser(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener usuario: {ex.Message}");
            }
        }

        // Update (Actualizar usuario)
        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, Users user)
        {
            try
            {
                user.Id = userId; // Asigna el ID desde la ruta al objeto de usuario
                await _userService.UpdateUser(user);
                return Ok("Usuario actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar usuario: {ex.Message}");
            }
        }

        // Delete (Eliminar usuario)
        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return Ok("Usuario eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar usuario: {ex.Message}");
            }
        }
    }
}

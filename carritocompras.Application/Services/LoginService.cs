using carritocompra.Infraestructure;
using carritocompra.Infraestructure.SeedWork;
using carritocompras.Application.SeedWorks;
using carritocompras.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carritocompras.Application.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly IUserService _usersService; // Puedes inyectar tu servicio de usuarios aquí

        public LoginService(IUnitOfWork unitOfWork,IUserService usersService) : base(unitOfWork)
        {
            _usersService = usersService;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await _usersService.GetUserByUsername(username);

            if (user != null && await _usersService.VerifyPassword(user, password))
            {
                // La autenticación fue exitosa
                return true;
            }

            // La autenticación falló
            return false;
        }
    }
}

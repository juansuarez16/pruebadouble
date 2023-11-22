using carritocompra.Entity.Entities.Users;
using carritocompra.Infraestructure.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using carritocompras.Application.SeedWorks;
using carritocompras.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using carritocompra.Entity.Entities;

namespace carritocompras.Application.Services
{
    public class UsersService : BaseService, IUserService
    {
        private readonly IRepository<Users> _userRepo;

        public UsersService(IUnitOfWork unitOfWork,IRepository<Users> userRepo) : base(unitOfWork)
        {
            _userRepo = userRepo;
        }
        public async Task<List<Users>> GetAllUsers()
        {
            return await _userRepo.GetAsyncAll();
        }
        public async Task<Users> GetUser(int userId)
        {
            var user = await _userRepo.GetAsync(userId);
            if (user == null)
            {
                throw new ("El usuario no existe. Redirigir al registro.");
            }

            return user;
        }

        // Resto de los métodos (GetAllUsers, CreateUser, UpdateUser, DeleteUser, etc.)
        // ...
        public string HashPassword(string password)
        {
            // Genera un hash seguro para la contraseña
            using (SHA256 sha256 = SHA256.Create()) // Cambia a SHA512 si prefieres SHA-512
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
            }
        }
        
        public async Task CreateUser(Users user)
        {
            string hashedPassword = HashPassword(user.Contraseña);
            user.Contraseña = hashedPassword;
            // Agregar lógica de validación de usuarios si es necesario
            _userRepo.Add(user);
            await UnitOfWork.SaveChangeAsync();
        }

        public async Task UpdateUser(Users user)
        {
            // Agregar lógica de validación y manejo de actualización si es necesario
             _userRepo.Update(user);
            await UnitOfWork.SaveChangeAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _userRepo.GetAsync(userId);
            if (user != null)
            if (user != null)
            {
                 _userRepo.Delete(user);

            }
        }
        public async Task<Users> GetUserByUsername(string username)
        {
            return await _userRepo.GetUserByUsernameAsync(username);
        }

        public async Task<bool> VerifyPassword(Users user, string password)
        {
            if (user == null)
            {
                return false;
            }

            bool passwordMatches = HashPassword(password) == user.Contraseña ? true:false;

            return passwordMatches;
        }
    }
}


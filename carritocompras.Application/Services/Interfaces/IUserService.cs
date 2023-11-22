using carritocompra.Entity.Entities;
using carritocompra.Entity.Entities.Users;

namespace carritocompras.Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<Users>> GetAllUsers();
        public Task<Users> GetUser(int userId);
        public Task CreateUser(Users user);
        public Task UpdateUser(Users user);
        public Task DeleteUser(int userId);

        public Task<Users> GetUserByUsername(string userName);

        public Task<bool> VerifyPassword(Users user, string password);
    }
}

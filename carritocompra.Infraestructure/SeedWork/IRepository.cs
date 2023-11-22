using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carritocompra.Infraestructure.SeedWork
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<T> GetAsync(int id);
        Task<List<T>> GetAsyncAll();
        Task<T> GetUserByUsernameAsync(string username);
        Task<bool> ConsultarPersonasPorIdentificacionAsync(string criterio);
    }
}

using carritocompra.Entity.Entities;
using carritocompra.Entity.Entities.Users;
using carritocompra.Entity.SeedWork;
using carritocompra.Infraestructure.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace carritocompra.Infraestructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private DbSet<T> _dbSet;

        private DbSet<T> DbSet => _dbSet ??= _dbContext.Set<T>();

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<T> GetAsync(int id)
        {
            return await DbSet.FirstOrDefaultAsync(_ => (_ as EntityBase).Id == id);
        }
        public async Task<List<T>> GetAsyncAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
        public async Task<T> GetUserByUsernameAsync(string username)
        {
            return await DbSet.FirstOrDefaultAsync(u => (u as Users).NombreUsuario == username);
        }
        public async Task<bool> ConsultarPersonasPorIdentificacionAsync(string criterio)
        {
            var personas = await DbSet.Where(p => (p as Persona).NumeroIdentificacion == criterio).ToListAsync();
            return personas.Any();
        }


    }
}

using carritocompra.Entity.Entities;
using carritocompra.Entity.Entities.Users;
using carritocompra.Infraestructure.SeedWork;
using carritocompras.Application.SeedWorks;
using carritocompras.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carritocompras.Application.Services
{
    public class PersonaService : BaseService, IPersonaRepository
    {
        private readonly IRepository<Persona> _personaRepo;

        public PersonaService(IUnitOfWork unitOfWork, IRepository<Persona> personaRepo) : base(unitOfWork)
        {
            _personaRepo = personaRepo;
        }

        public async Task<List<Persona>> ObtenerTodasPersonas()
        {
            return await _personaRepo.GetAsyncAll();
        }

        public async Task<Persona> ObtenerPersonaPorId(int personaId)
        {
            var persona = await _personaRepo.GetAsync(personaId);
            if (persona == null)
            {
                throw new Exception("La persona no existe. Redirigir al registro.");
            }

            return persona;
        }

        public async Task<Persona> CreatePersona(Persona persona)
        {
            var existingUser = await _personaRepo.ConsultarPersonasPorIdentificacionAsync(persona.NumeroIdentificacion);           

            if (existingUser)
            {
                return null;
            }
            _personaRepo.Add(persona);
            await UnitOfWork.SaveChangeAsync();
            return persona;
        }
        public async Task ActualizarPersona(Persona persona)
        {
            // Agregar lógica de validación y manejo de actualización si es necesario
            _personaRepo.Update(persona);
            await UnitOfWork.SaveChangeAsync();
        }

        public async Task EliminarPersona(int personaId)
        {
            var persona = await _personaRepo.GetAsync(personaId);
            if (persona != null)
            {
                _personaRepo.Delete(persona);
                await UnitOfWork.SaveChangeAsync();
            }
        }

        public async Task<List<Persona>> ConsultarPersonasPorCriterioAsync(string criterio)
        {
            var personas = await _personaRepo.GetAsyncAll();
            return personas.Where(p => p.Nombres.Contains(criterio) || p.Apellidos.Contains(criterio)).ToList();
        }

        public async Task<bool> ConsultarPersonasPorIdentificacionAsync(string criterio)
        {
            return await _personaRepo.ConsultarPersonasPorIdentificacionAsync(criterio);
        }
    }


}

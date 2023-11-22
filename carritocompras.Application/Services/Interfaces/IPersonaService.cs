using carritocompra.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carritocompras.Application.Services.Interfaces
{
    public interface IPersonaRepository
    {
        Task<List<Persona>> ObtenerTodasPersonas();
        Task<Persona> ObtenerPersonaPorId(int personaId);        
        Task ActualizarPersona(Persona persona);
        Task EliminarPersona(int personaId);
        Task<List<Persona>> ConsultarPersonasPorCriterioAsync(string criterio);
        Task<Persona> CreatePersona(Persona persona);
        Task<bool> ConsultarPersonasPorIdentificacionAsync(string criterio);
    }


}

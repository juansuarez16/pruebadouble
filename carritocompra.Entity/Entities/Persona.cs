using carritocompra.Entity.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carritocompra.Entity.Entities
{
    public class Persona : EntityBase
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Email { get; set; }
        public string TipoIdentificacion { get; set; }      
        public string NumeroIdentificacionTipoConcatenado { get; set; }
        public string NombresApellidosConcatenados { get; set; }

        
    }
}

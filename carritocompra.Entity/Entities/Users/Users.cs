using carritocompra.Entity.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carritocompra.Entity.Entities.Users
{
    [Table("Users")]
    public class Users: EntityBase
    {
        
        [MaxLength(255)]
        public string NombreUsuario { get; set; }

        [MaxLength(500)]
        public string Contraseña { get; set; }
        [ForeignKey("Persona")]
        public int PersonaId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Sala
    {
        public int IdSala { get; set; }
        public string Descripcion { get; set; }
        public int Capacidad { get; set; }
        public bool Activo { get; set; }
        public string FechaRegistro { get; set; }
        
    }
}

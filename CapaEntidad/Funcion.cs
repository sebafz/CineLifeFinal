using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Funcion
    {

        public int IdFuncion { get; set; }
        public int IdPelicula { get; set; }
        public int IdSala { get; set; }

        public string NombrePelicula { get; set; }
        public string DescripcionSala { get; set; }

        public bool Activo { get; set; }

        public string Horario { get; set; }

        //public string FechaVencimiento { get; set; }
        //public string FechaRegistro { get; set; }

    }
}

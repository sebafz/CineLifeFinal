using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Horario
    {

        public int IdHorario { get; set; }
        public string Hora { get; set; }
        public bool Activo { get; set; }
        public string FechaRegistro { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Sede
    {
        public int IdSede { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public Localidad oLocalidad { get; set; }

    }
}

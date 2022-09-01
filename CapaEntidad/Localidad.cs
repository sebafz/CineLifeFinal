using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Localidad
    {
        public int IdLocalidad { get; set; }
        public Provincia oProvincia { get; set; }
        public string Identificador { get; set; }
        public string Descripcion { get; set; }
    }
}

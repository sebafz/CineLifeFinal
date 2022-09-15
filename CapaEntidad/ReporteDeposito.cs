using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteDeposito
    {
        public int IdDeposito { get; set; }
        public int IdSede { get; set; }
        public string Nombre { get; set; }

        public string Area { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public string Descripcion { get; set; }
        public string DescripcionProvincia { get; set; }
        public bool Activo { get; set; }

    }
}
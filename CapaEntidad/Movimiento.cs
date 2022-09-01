using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Movimiento
    {
        public int IdMovimiento { get; set; }
        public Usuario oUsuario { get; set; }
        public Proveedor oProveedor { get; set; }

        public int Tipo { get; set; }
        public string NumTransaccion { get; set; }
        public float Total { get; set; }
        public string FechaMovimiento { get; set; }
    }
}

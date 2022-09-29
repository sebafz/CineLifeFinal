using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Comprobante
    {
        public int IdComprobante { get; set; }
        public Usuario oUsuario { get; set; }
        public Proveedor oProveedor { get; set; }
        public Sede oSede { get; set; }
        public string Letra { get; set; }
        public int Ingreso { get; set; }
        public int Tipo { get; set; }
        public int Numero { get; set; }
        public decimal Total { get; set; }
        public string Fecha { get; set; }
    }
}

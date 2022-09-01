using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int CUIL { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
        public string FechaRegistro { get; set; }
        public TipoProveedor oTipoProveedor { get; set; }
        public Localidad oLocalidad { get; set; }
    }
}

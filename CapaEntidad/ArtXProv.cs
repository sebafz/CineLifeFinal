using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ArtXProv
    {
        public int IdArtXProv { get; set; }
        public Producto oProducto { get; set; }
        public Proveedor oProveedor { get; set; }
    }
}

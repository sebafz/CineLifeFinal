using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleMovimiento
    {
        public int IdDetalleMovimiento { get; set; }
        public Movimiento oMovimiento { get; set; }
        public Producto oProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}

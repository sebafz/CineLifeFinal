using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ArtXDeposito
    {
        public int IdArtXDeposito { get; set; }
        public Producto oProducto { get; set; }
        public Deposito oDeposito { get; set; }
        public int Stock { get; set; }
        public int StockMaximo { get; set; }
        public int StockMinimo { get; set; }
        public int PuntoDePedido { get; set; }
        public bool Activo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleComprobante
    {
        public int IdDetalleComprobante { get; set; }
        public Comprobante oComprobante { get; set; }
        public Producto oProducto { get; set; }
        public ArtXDeposito oArtXDeposito { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public Deposito oDepositoDestino { get; set; }
    }
}

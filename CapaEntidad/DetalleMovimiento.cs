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
        public ArtXDeposito IdArtXDeposito { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public float Descuento { get; set; }
        public Deposito oDepositoDestino { get; set; }
    }
}

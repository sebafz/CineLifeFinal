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
        public Deposito oDeposito { get; set; }
        public Deposito oDepositoDestino { get; set; }
        public Usuario oUsuario { get; set; }
        public Comprobante oComprobante { get; set; }
        public MotivoMovimiento oMotivoMovimiento { get; set; }
        public int Ingreso { get; set; }
        public int Numero { get; set; }
        public decimal Total { get; set; }
        public string Fecha { get; set; }
    }
}

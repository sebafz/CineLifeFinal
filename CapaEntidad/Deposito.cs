using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Deposito
    {
        public int IdDeposito { get; set; }
        public Sede oSede { get; set; }
        public string Nombre { get; set; }
    }
}

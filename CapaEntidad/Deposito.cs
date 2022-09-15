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
        public int Sede { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}

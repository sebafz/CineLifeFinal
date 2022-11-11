using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleBoleto
    {
        public int IdDetalleBoleto { get; set; }
        public Butaca oButaca { get; set; }
        public Comprobante oComprobante {get;set;}
        public Funcion oFuncion { get; set; }
        public decimal Precio { get; set; }

    }
}

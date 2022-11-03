using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Butaca
    {
        public int IdButaca { get; set; }   
        public Sala oSala { get; set; }
        public int Numero { get; set; }
        public string Fecha { get; set; }
    }
}

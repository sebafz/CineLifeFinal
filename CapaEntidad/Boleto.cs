using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Boleto
    {
        public int IdBoleto { get; set; }
        public Funcion oFuncion  { get; set; }
        public Cliente oCliente { get; set; }
        public string Numero { get; set; }
        public string Fecha { get; set; }
    }
}

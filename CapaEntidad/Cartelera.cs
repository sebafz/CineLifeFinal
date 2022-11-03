using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cartelera
    {
        public int IdCartelera { get; set; }
        public Funcion oFuncion { get; set; }
        public string Hora { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public bool Activo { get; set; }
    }
}

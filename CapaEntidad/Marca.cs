using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Marca
    {
        public int IdMarca { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public string FechaRegistro { get; set; }
        public int Tipo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Funcion
    {

        public int IdFuncion { get; set; }
        public Pelicula oPelicula { get; set; }
        public Idioma oIdioma { get; set; }
        public Sala oSala { get; set; }
        public decimal Precio { get; set; }

        public bool Activo { get; set; }


    }
}

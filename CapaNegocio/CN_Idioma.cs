using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Idioma
    {
        private CD_Idioma objCapaDato = new CD_Idioma();
        public List<Idioma> Listar()
        {
            return objCapaDato.Listar();
        }
        public List<Idioma> ListarXFuncion(int id, string fecha)
        {
            return objCapaDato.ListarXFuncion(id, fecha);
        }
    }
}

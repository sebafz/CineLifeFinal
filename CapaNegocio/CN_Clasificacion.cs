using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Clasificacion
    {
        private CD_Clasificacion objCapaDato = new CD_Clasificacion();
        public List<Clasificacion> Listar()
        {
            return objCapaDato.Listar();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Movimiento
    {
        private CD_Movimiento objCapaDato = new CD_Movimiento();
        public List<Movimiento> Listar()
        {
            return objCapaDato.Listar();
        }
    } 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_MedioPago
    {
        private CD_MedioPago objCapaDato = new CD_MedioPago();
        public List<MedioPago> Listar()
        {
            return objCapaDato.Listar();
        }
    }
}

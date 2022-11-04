using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_MotivoMovimiento
    {
        private CD_MotivoMovimiento objCapaDato = new CD_MotivoMovimiento();
        public List<MotivoMovimiento> Listar()
        {
            return objCapaDato.Listar();
        }
    }
}

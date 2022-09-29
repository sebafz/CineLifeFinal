using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_ArtXDeposito
    {
        private CD_ArtXDeposito objCapaDato = new CD_ArtXDeposito();
        public int Registrar(ArtXDeposito obj, out string Mensaje)
        {

                return objCapaDato.Registrar(obj, out Mensaje);

        }

        public bool Editar(ArtXDeposito obj, out string Mensaje)
        {
            
                return objCapaDato.Editar(obj, out Mensaje);
            
        }

    }
}

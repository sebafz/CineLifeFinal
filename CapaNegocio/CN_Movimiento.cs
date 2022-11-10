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
        public int RegistrarComprobante(Comprobante comp, int mot, int ingreso, out string Mensaje)
        {

            Mensaje = string.Empty;

            return objCapaDato.RegistrarComprobante(comp, mot, ingreso, out Mensaje);

        }
    } 
}

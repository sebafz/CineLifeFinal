using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Comprobante
    {
        private CD_Comprobante objCapaDato = new CD_Comprobante();
        public List<Comprobante> Listar(int tipo, int ingreso)
        {
            return objCapaDato.Listar(tipo, ingreso);
        }
        public bool Vincular(int id, int tipo, out string Mensaje)
        {
            return objCapaDato.Vincular(id, tipo, out Mensaje);
        }
        public int Registrar(Comprobante comp, List<Producto> list, out string Mensaje)
        {

        return objCapaDato.Registrar(comp, list, out Mensaje);

        }

    }
}

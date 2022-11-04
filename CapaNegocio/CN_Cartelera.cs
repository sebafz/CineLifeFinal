using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Cartelera
    {
        private CD_Cartelera objCapaDato = new CD_Cartelera();
        public List<Cartelera> Listar()
        {
            return objCapaDato.Listar();
        }
        public int Registrar(Cartelera obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            return objCapaDato.Registrar(obj, out Mensaje);

        }

        public bool Editar(Cartelera obj, out string Mensaje)
        {

            Mensaje = string.Empty;
            return objCapaDato.Editar(obj, out Mensaje);

        }
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}

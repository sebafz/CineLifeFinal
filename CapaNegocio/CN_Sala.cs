using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Sala
    {



        private CD_Sala objCapaDato = new CD_Sala();
        public List<Sala> Listar()
        {
            return objCapaDato.Listar();
        }

        public List<Sala> ListarActivas()
        {
            return objCapaDato.ListarActivas();
        }
        public List<Sala> ListarXFuncion(int idfuncion)
        {
            return objCapaDato.ListarXFuncion(idfuncion);
        }

        public int Registrar(Sala obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            return objCapaDato.Registrar(obj, out Mensaje);

        }

        public bool Editar(Sala obj, out string Mensaje)
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

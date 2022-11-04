using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Funcion
    {
        private CD_Funcion objCapaDato = new CD_Funcion();
        public List<Funcion> Listar()
        {
            return objCapaDato.Listar();
        }

        public List<Funcion> ListarXPelicula(int idpelicula)
        {
            return objCapaDato.ListarXPelicula(idpelicula);
        }
        public int Registrar(Funcion obj, out string Mensaje)
        {

            Mensaje = string.Empty;

             return objCapaDato.Registrar(obj, out Mensaje);

        }

        public bool Editar(Funcion obj, out string Mensaje)
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

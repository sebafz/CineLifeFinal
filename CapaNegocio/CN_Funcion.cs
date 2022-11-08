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

        public List<string> ObtenerFechas(int idpelicula)
        {
            return objCapaDato.ObtenerFechas(idpelicula);
        }
        public List<string> ObtenerButacasOcupadas(int idfuncion)
        {
            return objCapaDato.ObtenerButacasOcupadas(idfuncion);
        }
        public List<Funcion> ObtenerHoras(int idpelicula, string fecha, int ididioma)
        {
            return objCapaDato.ObtenerHoras(idpelicula, fecha, ididioma);
        }
        public List<Funcion> ObtenerFuncion(int idfuncion)
        {
            return objCapaDato.ObtenerFuncion(idfuncion);
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

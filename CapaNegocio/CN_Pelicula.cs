using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Pelicula
    {
        private CD_Pelicula objCapaDato = new CD_Pelicula();
        public List<Pelicula> Listar()
        {
            return objCapaDato.Listar();
        }
        public List<Pelicula> ObtenerPeliculasTienda(int idGenero, int idClasificacion, int nroPagina, int obtenerRegistros, out int TotalRegistros, out int TotalPaginas)
        {
            return objCapaDato.ObtenerPeliculas(idGenero, idClasificacion, nroPagina, obtenerRegistros, out TotalRegistros, out TotalPaginas);

        }
        public List<Pelicula> ListarActivas()
        {
            return objCapaDato.ListarActivas();
        }

        public int Registrar(Pelicula obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            return objCapaDato.Registrar(obj, out Mensaje);


        }

        public bool Editar(Pelicula obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool GuardarDatosImagen(Pelicula obj, out string Mensaje)
        {

            return objCapaDato.GuardarDatosImagen(obj, out Mensaje);
        }



        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
        
    }
}

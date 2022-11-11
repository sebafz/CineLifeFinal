using System;
using System.Collections.Generic;
using System.Globalization;
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
            CultureInfo cultura = new CultureInfo("es-MX");
            Mensaje = string.Empty;
            if (DateTime.ParseExact(obj.FechaDesde,"mm/dd/yyyy", cultura) > DateTime.ParseExact(obj.FechaHasta, "mm/dd/yyyy", cultura))
            {
                Mensaje = "Fecha desde debe ser menor que fecha hasta";
            }

            if (Convert.ToDateTime(obj.FechaDesde, cultura)< Convert.ToDateTime(obj.oPelicula.FechaIngreso, cultura) || Convert.ToDateTime(obj.FechaHasta, cultura) > Convert.ToDateTime(obj.oPelicula.FechaEgreso, cultura))
            {
                Mensaje ="El rango de fechas no es válido";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {

                return 0;
            }
        }

        public bool Editar(Cartelera obj, out string Mensaje)
        {

            CultureInfo cultura = new CultureInfo("es-MX");
            Mensaje = string.Empty;
            if (DateTime.ParseExact(obj.FechaDesde, "mm/dd/yyyy", cultura) > DateTime.ParseExact(obj.FechaHasta, "mm/dd/yyyy", cultura))
            {
                Mensaje = "Fecha desde debe ser menor que fecha hasta";
            }

            if (Convert.ToDateTime(obj.oPelicula.FechaIngreso, cultura) > Convert.ToDateTime(obj.FechaDesde, cultura) && Convert.ToDateTime(obj.FechaHasta, cultura) > Convert.ToDateTime(obj.oPelicula.FechaEgreso, cultura))
            {
                Mensaje = "El rango de fechas no es válido";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {

                return false;
            }

        }
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}

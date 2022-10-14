using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Sede
    {
        private CD_Sede objCapaDato = new CD_Sede();
        public List<Sede> ObtenerSede(string idlocalidad)
        {

            return objCapaDato.ObtenerSede(idlocalidad);
        }

        public List<Sede> ObtenerTodasLasSedes()
        {

            return objCapaDato.ObtenerTodasLasSedes();
        }
        public List<Sede> ObtenerActivas()
        {

            return objCapaDato.ObtenerActivas();
        }

        public List<Sede> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Sede obj, out string Mensaje)
        {

            Mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.Nombre, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "El nombre no puede contener números o símbolos";
            }

            else if (string.IsNullOrEmpty(obj.Direccion) || string.IsNullOrWhiteSpace(obj.Direccion))
            {
                Mensaje = "La dirección no puede ser vacía";
            }
            else if (!Regex.IsMatch(obj.Direccion, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "La dirección no puede contener números o símbolos";
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

        public bool Editar(Sede obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.Nombre, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "El nombre no puede contener números o símbolos";
            }
            else if (string.IsNullOrEmpty(obj.Direccion) || string.IsNullOrWhiteSpace(obj.Direccion))
            {
                Mensaje = "La dirección no puede ser vacía";
            }
            else if (!Regex.IsMatch(obj.Direccion, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "La dirección no puede contener números o símbolos";
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

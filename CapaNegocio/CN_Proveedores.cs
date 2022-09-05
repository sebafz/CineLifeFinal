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
    public class CN_Proveedores
    {

        private CD_Proveedores objCapaDato = new CD_Proveedores();

        public List<Proveedor> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {

            Mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.Nombres, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "El nombre no puede contener números o símbolos";
            }

            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.Apellidos, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "El apellido no puede contener números o símbolos";
            }

            if (String.IsNullOrEmpty(obj.CUIL.ToString()) || string.IsNullOrWhiteSpace(obj.CUIL.ToString()))
            {
                Mensaje = "El CUIL no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.CUIL.ToString(), @"^[0-9]+$"))
            {
                Mensaje = "El CUIL no puede contener letras o símbolos";
            }

            if (string.IsNullOrEmpty(obj.Telefono) || string.IsNullOrWhiteSpace(obj.Telefono))
            {
                Mensaje = "El teléfono no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.Telefono, @"^[0-9]+$"))
            {
                Mensaje = "El teléfono no puede contener números o símbolos";
            }

            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo no puede ser vacío";
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

        public bool Editar(Proveedor obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.Nombres, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "El nombre no puede contener números o símbolos";
            }

            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.Apellidos, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "El apellido no puede contener números o símbolos";
            }

            if (String.IsNullOrEmpty(obj.CUIL.ToString()) || string.IsNullOrWhiteSpace(obj.CUIL.ToString()))
            {
                Mensaje = "El DNI no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.CUIL.ToString(), @"^[0-9]+$"))
            {
                Mensaje = "El DNI no puede contener letras o símbolos";
            }

            if (string.IsNullOrEmpty(obj.Telefono) || string.IsNullOrWhiteSpace(obj.Telefono))
            {
                Mensaje = "El teléfono no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.Telefono, @"^[0-9]+$"))
            {
                Mensaje = "El teléfono no puede contener números o símbolos";
            }

            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo no puede ser vacío";
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

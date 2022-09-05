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
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> Listar() {
            return objCapaDato.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje) {

            Mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres)) {
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

            if (String.IsNullOrEmpty(obj.DNI.ToString()) || string.IsNullOrWhiteSpace(obj.DNI.ToString()))
            {
                Mensaje = "El DNI no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.DNI.ToString(), @"^[0-9]+$"))
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


            if (string.IsNullOrEmpty(Mensaje))
            {

                string clave = CN_Recursos.GenerarClave();

                string asunto = "CineLife: Te damos la bienvenida";
                string mensaje_correo = "<h3 style='background-color: #fdf3e4;padding: 15px;margin: 0 5px 0;color: #de4f52;border-radius: 15px;'>Bienvenido a CineLife</h3>" +
                    "<h4 style='padding-left: 10px; color:black'>Este correo electrónico ha sido registrado como administrador de CineLife</h4>" +
                    "<p style='margin: 15px 8px'>Tu contraseña para acceder es: </p>" +
                    "<h1 style='color: #de4f52; margin-left: 8px'>!clave!</h1>" +
                    "<p style='margin: 15px 8px 5px'>Si no fuiste vos, por favor no hagas caso a este mensaje.</p>" +
                    "<p style='margin: 15px 8px'>Gracias, equipo de CineLife.</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);


                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);

                if (respuesta)
                {

                    obj.Clave = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.Registrar(obj, out Mensaje);
                }
                else {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }


            }
            else {

                return 0;
            }


            
        }

        public bool Editar(Usuario obj, out string Mensaje) {

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

            if (String.IsNullOrEmpty(obj.DNI.ToString()) || string.IsNullOrWhiteSpace(obj.DNI.ToString()))
            {
                Mensaje = "El DNI no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.DNI.ToString(), @"^[0-9]+$"))
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

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.Editar(obj, out Mensaje);
            }
            else {
                return false;
            }
        }


        public bool Eliminar(int id, out string Mensaje) {

            return objCapaDato.Eliminar(id, out Mensaje);
        }


        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {

            return objCapaDato.CambiarClave(idusuario,nuevaclave, out Mensaje);
            
        }


        public bool ReestablecerClave(int idusuario,string correo, out string Mensaje)
        {

            Mensaje = string.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idusuario,CN_Recursos.ConvertirSha256(nuevaclave), out Mensaje);

            if (resultado)
            {

                string asunto = "Fun House: Restablecer contraseña";
                string mensaje_correo = "<h3 style='background-color: #f5f6fa;padding: 15px;margin: 0 5px 0;color: #004aad;border-radius: 15px;'>Restablecer contraseña</h3>" +
                    "<h4 style='padding-left: 10px; color:black'>Recibimos una solicitud de cambio de contraseña</h4>" +
                    "<p style='margin: 15px 8px'>Tu nueva contraseña para acceder es: </p>" +
                    "<h1 style='color: #004aad; margin-left: 8px'>!clave!</h1>" +
                    "<p style='margin: 15px 8px 5px'>Ingresala en nuestro sitio para contnuar.</p>" +
                    "<p style='margin: 8px'>Si no fuiste vos, por favor no hagas caso a este mensaje y asegurate de que todavía podés acceder a tu cuenta.</p>" +
                    "<p style='margin: 15px 8px'>Gracias, equipo de Fun House.</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);


                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {

                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }

            }
            else {
                Mensaje = "No se pudo restablecer la contraseña";

                return false;
            }


        }
    }
}

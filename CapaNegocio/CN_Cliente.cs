﻿using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objCapaDato = new CD_Cliente();


        public int Registrar(Cliente obj, out string Mensaje)
        {

            Mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre no puede ser vacio";
            }
            else if (!Regex.IsMatch(obj.Nombres, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "El nombre no puede contener números o símbolos";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido no puede ser vacio";
            }
            else if (!Regex.IsMatch(obj.Apellidos, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "El apellido no puede contener números o símbolos";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo no puede ser vacío";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {

                string asunto = "CineLife: Te damos la bienvenida";
                string mensaje_correo = "<h3 style='background-color: #fdf3e4;padding: 15px;margin: 0 5px 0;color: #c4a475;border-radius: 15px;'>Bienvenido a Cinelife</h3>" +
                    "<h4 style='padding-left: 10px; color:black'>¡Ya podés disfrutar de las mejores películas!</h4>" +
                    "<p style='margin: 15px 8px'>Ingresá a cinelife.com.ar y mirá todo lo que tenemos para vos.</p>" +
                    "<p style='margin: 15px 8px 5px; color: #c4a475'>CineLife ¡Y que comience la función!</p>";
                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    obj.Clave = CN_Recursos.ConvertirSha256(obj.Clave);
                    return objCapaDato.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo de bienvenida";
                    return 0;
                }
                

            }
            else
            {

                return 0;
            }



        }

        public Cliente VerPerfil(int idcliente)
        {
            return objCapaDato.VerPerfil(idcliente);
        }

        public int RegistrarEditar(Cliente obj, out string Mensaje)
        {

            Mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre del usuario no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.Nombres, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "El nombre no puede contener números o símbolos";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido del usuario no puede ser vacío";
            }
            else if (!Regex.IsMatch(obj.Apellidos, @"^[a-zA-Z ]+$"))
            {
                Mensaje = "El apellido no puede contener números o símbolos";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo no puede ser vacío";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {

                string clave = CN_Recursos.GenerarClave();

                string asunto = "Fun House: Te damos la bienvenida";
                string mensaje_correo = "<h3 style='background-color: #f5f6fa;padding: 15px;margin: 0 5px 0;color: #004aad;border-radius: 15px;'>Bienvenido a Fun House</h3>" +
                    "<h4 style='padding-left: 10px; color:black'>Este correo electrónico ha sido registrado como cliente de Fun House</h4>" +
                    "<p style='margin: 15px 8px'>Tu contraseña para acceder es: </p>" +
                    "<h1 style='color: #004aad; margin-left: 8px'>!clave!</h1>" +
                    "<p style='margin: 15px 8px 5px'>Si no fuiste vos, por favor no hagas caso a este mensaje.</p>" +
                    "<p style='margin: 15px 8px'>Gracias, equipo de Fun House.</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);


                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);

                if (respuesta)
                {

                    obj.Clave = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.RegistrarEditar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }


            }
            else
            {

                return 0;
            }



        }

        public List<Cliente> Listar()
        {
            return objCapaDato.Listar();
        }

        public List<Cliente> ListarActivos()
        {
            return objCapaDato.ListarActivos();
        }

        public bool Editar(Cliente obj, out string Mensaje)
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
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo no puede ser vacío";
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



        public bool CambiarClave(int idcliente, string nuevaclave, out string Mensaje)
        {

            return objCapaDato.CambiarClave(idcliente, nuevaclave, out Mensaje);
        }


        public bool ReestablecerClave(int idcliente, string correo, out string Mensaje)
        {

            Mensaje = string.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idcliente, CN_Recursos.ConvertirSha256(nuevaclave), out Mensaje);

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
            else
            {
                Mensaje = "No se pudo reestablecer la contraseña";

                return false;
            }


        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;


using System.Web.Security;
using System.Web.UI;

namespace CapaPresentacionAdmin.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();
        }
        public ActionResult Reestablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario oUsuario = new Usuario();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.Correo == correo && u.Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();


            if (oUsuario == null)
            {

                ViewBag.Error = "Correo o contraseña incorrectos";
                return View();
            }
            else {

                if (oUsuario.Reestablecer) {

                    TempData["IdUsuario"] = oUsuario.IdUsuario;
                    TempData["ClaveCliente"] = clave;
                    return RedirectToAction("CambiarClave");

                }


                FormsAuthentication.SetAuthCookie(oUsuario.Correo, false);

                ViewBag.Error = null;

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string idusuario, string claveactual, string nuevaclave, string confirmarclave)
        {
            
            Usuario oUsuario = new Usuario();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.IdUsuario == int.Parse(idusuario)).FirstOrDefault();

            if (oUsuario.Clave != CN_Recursos.ConvertirSha256(claveactual))
            {
                TempData["IdUsuario"] = idusuario;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaclave != confirmarclave) {

                TempData["IdUsuario"] = idusuario;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            ViewData["vclave"] = "";


            nuevaclave = CN_Recursos.ConvertirSha256(nuevaclave);


            string mensaje = string.Empty;

            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(idusuario), nuevaclave, out mensaje);

            if (respuesta)
            {
                //Response.Write("<script>alert('Tu contraseña se restabeció con éxito, ahora podés iniciar sesión');window.location.href = 'Index';</script>");
                ViewBag.Message = "true";
                return View();
            }
            else {

                TempData["IdUsuario"] = idusuario;

                ViewBag.Error = mensaje;
                return View();
            }

        }


        [HttpPost]
        public ActionResult Reestablecer(string correo) {

            Usuario ousurio = new Usuario();

            ousurio = new CN_Usuarios().Listar().Where(item => item.Correo == correo).FirstOrDefault();

            if (ousurio == null) {


                ViewBag.Error = "No se encontró un usuario relacionado a ese correo";
                return View();
            }


            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().ReestablecerClave(ousurio.IdUsuario, correo, out mensaje);

            if (respuesta)
            {

                ViewBag.Error = null;
                //Response.Write("<script>alert('¡Listo! Se te envió un mail para restablecer la contraseña');window.location.href = 'Index';</script>");
                ViewBag.Message = "true";
                return View();

            }
            else {

                ViewBag.Error = mensaje;
                Response.Write("<script>alert('Error');window.location.href = 'Index';</script>");
                return View();
            }

        
        
        }

        public ActionResult CerrarSesion() {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");

        }



    }
}
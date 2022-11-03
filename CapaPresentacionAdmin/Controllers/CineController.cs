using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;

namespace CapaPresentacionAdmin.Controllers
{
    public class CineController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Peliculas()
        {
            return View();
        }
        public ActionResult Boletos()
        {
            return View();
        }
        public ActionResult Cartelera()
        {
            return View();
        }
        public ActionResult Salas()
        {
            return View();
        }
        public ActionResult Funciones()
        {
            return View();
        }

        // ++++++++++++++++ PELICULAS ++++++++++++++++++++

        #region PELICULAS;
        [HttpGet]
        public JsonResult ListarPeliculas()
        {
            List<Pelicula> oLista = new List<Pelicula>();
            oLista = new CN_Pelicula().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPeliculasActivas()
        {
            List<Pelicula> oLista = new List<Pelicula>();
            oLista = new CN_Pelicula().ListarActivas();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ImagenPelicula(int id)
        {

            bool conversion;
            Pelicula oproducto = new CN_Pelicula().Listar().Where(p => p.IdPelicula == id).FirstOrDefault();

            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oproducto.RutaImagen, oproducto.NombreImagen), out conversion);


            return Json(new
            {
                conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(oproducto.NombreImagen)

            },
             JsonRequestBehavior.AllowGet
            );

        }

        [HttpPost]
        public JsonResult EliminarPelicula(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Pelicula().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> GuardarPelicula(string objeto, HttpPostedFileBase archivoImagen)
        {

            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Pelicula oPelicula = new Pelicula();
            oPelicula = JsonConvert.DeserializeObject<Pelicula>(objeto);


            if (oPelicula.IdPelicula == 0)
            {
                int idPeliculaGenerado = new CN_Pelicula().Registrar(oPelicula, out mensaje);

                if (idPeliculaGenerado != 0)
                {
                    oPelicula.IdPelicula = idPeliculaGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }
            }
            else
            {
                operacion_exitosa = new CN_Pelicula().Editar(oPelicula, out mensaje);
            }


            if (operacion_exitosa)
            {

                if (archivoImagen != null)
                {

                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];

                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oPelicula.IdPelicula.ToString(), extension);

                    //string ruta_guardar = await cnfirebase.SubirStorage(archivoImagen.InputStream, nombre_imagen);

                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));

                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardar_imagen_exito = false;
                    }

                    guardar_imagen_exito = ruta_guardar != "" ? true : false;

                    if (guardar_imagen_exito)
                    {

                        oPelicula.RutaImagen = ruta_guardar;
                        oPelicula.NombreImagen = nombre_imagen;
                        bool rspta = new CN_Pelicula().GuardarDatosImagen(oPelicula, out mensaje);
                    }
                    else
                    {

                        mensaje = "Se guardaró la película pero hubo problemas con la carga de la imagen";
                    }


                }
            }



            return Json(new { operacionExitosa = operacion_exitosa, idGenerado = oPelicula.IdPelicula, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion;

        // ++++++++++++++++ SALAS ++++++++++++++++++++
        #region SALAS;
        [HttpGet]
        public JsonResult ListarSalas()
        {
            List<Sala> oLista = new List<Sala>();
            oLista = new CN_Sala().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarSalasActivas()
        {
            List<Sala> oLista = new List<Sala>();
            oLista = new CN_Sala().ListarActivas();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarSala(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Sala().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarSala(Sala objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdSala == 0)
            {
                resultado = new CN_Sala().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Sala().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion;

        // ++++++++++++++++ FUNCIONES ++++++++++++++++++++
        #region FUNCIONES;
        [HttpGet]
        public JsonResult ListarFunciones()
        {
            List<Funcion> oLista = new List<Funcion>();
            oLista = new CN_Funcion().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarFuncion(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Funcion().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarFuncion(Funcion objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdFuncion == 0)
            {
                resultado = new CN_Funcion().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Funcion().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarFuncionesXPelicula(int id)
        {
            List<Funcion> oLista = new List<Funcion>();
            oLista = new CN_Funcion().ListarXPelicula(id);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        #endregion;

        // ++++++++++++++++ CARTELERA ++++++++++++++++++++
        #region CARETLERA;
        [HttpGet]
        public JsonResult ListarCartelera()
        {
            List<Cartelera> oLista = new List<Cartelera>();
            oLista = new CN_Cartelera().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        #endregion;

        // ++++++++++++++++ EXTRAS ++++++++++++++++++++
        #region EXTRAS;
        [HttpGet]
        public JsonResult ListarClasificaciones()
        {

            List<Clasificacion> oLista = new List<Clasificacion>();

            oLista = new CN_Clasificacion().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ListarGeneros()
        {

            List<Genero> oLista = new List<Genero>();

            oLista = new CN_Genero().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ListarIdiomas()
        {

            List<Idioma> oLista = new List<Idioma>();

            oLista = new CN_Idioma().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }
        #endregion;
    }
}
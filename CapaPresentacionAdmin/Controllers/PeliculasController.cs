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
    public class PeliculasController : Controller
    {
        public ActionResult Peliculas()
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



        #region Peliculas

        [HttpGet]
        public JsonResult ListarPeliculas()
        {
            List<Pelicula> oLista = new List<Pelicula>();
            oLista = new CN_Pelicula().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public async Task<JsonResult>GuardarPelicula(string objeto, HttpPostedFileBase archivoImagen)
        {

            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Pelicula oProducto = new Pelicula();
            oProducto = JsonConvert.DeserializeObject<Pelicula>(objeto);
        


            if (oProducto.IdPelicula == 0)
            {
                int idProductoGenerado = new CN_Pelicula().Registrar(oProducto, out mensaje);

                if (idProductoGenerado != 0)
                {
                    oProducto.IdPelicula = idProductoGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }
            }
            else
            {
                operacion_exitosa = new CN_Pelicula().Editar(oProducto, out mensaje);
            }


            if (operacion_exitosa)
            {

                if (archivoImagen != null)
                {

                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];

                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProducto.IdPelicula.ToString(), extension);

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

                        oProducto.RutaImagen = ruta_guardar;
                        oProducto.NombreImagen = nombre_imagen;
                        bool rspta = new CN_Pelicula().GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else
                    {

                        mensaje = "Se guardaró el producto pero hubo problemas con la carga de la imagen";
                    }


                }
            }




            return Json(new { operacionExitosa = operacion_exitosa, idGenerado = oProducto.IdPelicula, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ImagenProducto(int id)
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

            return Json(new { ruta = oproducto.RutaImagen }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarPelicula(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Pelicula().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion




        #region Sala


        [HttpGet]
        public JsonResult ListarSalas()
        {
            List<Sala> oLista = new List<Sala>();
            oLista = new CN_Sala().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]

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

        [HttpPost]
        public JsonResult EliminarSala(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Sala().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        #endregion
        public JsonResult ListarFunciones()
        {
            List<Funcion> oLista = new List<Funcion>();
            oLista = new CN_Funcion().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }



    }
}
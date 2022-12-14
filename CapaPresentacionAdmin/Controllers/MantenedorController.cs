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
    [Authorize]
    public class MantenedorController : Controller
    {
        private CN_FireBase cnfirebase = new CN_FireBase();

        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }

        public ActionResult Deposito()
        {
            return View();
        }

        public ActionResult ArtXDeposito()
        {
            return View();
        }
        public ActionResult Movimientos()
        {
            return View();
        }

        // ++++++++++++++++ CATEGORIA ++++++++++++++++++++

        #region CATEGORIA
        [HttpGet]
        public JsonResult ListarCategorias()
        {

            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {

                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().Editar(objeto, out mensaje);

            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        // ++++++++++++++++ MARCA ++++++++++++++++++++

        #region MARCA
        [HttpGet]
        public JsonResult ListarMarca()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new CN_Marca().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarca(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdMarca == 0)
            {
                resultado = new CN_Marca().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Marca().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Marca().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        // ++++++++++++++++ PRODUCTO ++++++++++++++++++++
        #region PRODUCTO
        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProductosVacio()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().ListarVacio();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProductosActivos()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().ListarActivos();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProductosXProveedor(int id)
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().ListarXProveedor(id);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProductosXComprobante(int id)
        {
            List<DetalleComprobante> oLista = new List<DetalleComprobante>();
            oLista = new CN_Producto().ListarXComprobante(id);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProductoXDeposito(int id)
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().ListarXDeposito(id);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> GuardarProducto(string objeto, HttpPostedFileBase archivoImagen)
        {

            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto);

            decimal precio;

            if (decimal.TryParse(oProducto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out precio))
            {

                oProducto.Precio = precio;
            }
            else {

                return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe ser ####.##" },JsonRequestBehavior.AllowGet);
            }


            if (oProducto.IdProducto == 0)
            {
                int idProductoGenerado = new CN_Producto().Registrar(oProducto, out mensaje);

                if (idProductoGenerado != 0)
                {
                    oProducto.IdProducto = idProductoGenerado;
                }
                else {
                    operacion_exitosa = false;
                }
            }
            else
            {
                operacion_exitosa = new CN_Producto().Editar(oProducto, out mensaje);
            }


            if (operacion_exitosa) {

                if (archivoImagen != null) {

                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];

                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProducto.IdProducto.ToString(), extension);

                    //string ruta_guardar = await cnfirebase.SubirStorage(archivoImagen.InputStream, nombre_imagen);

                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));

                    }
                    catch (Exception ex) {
                        string msg = ex.Message;
                        guardar_imagen_exito = false;
                    }

                    guardar_imagen_exito = ruta_guardar != "" ? true : false;

                    if (guardar_imagen_exito)
                    {

                        oProducto.RutaImagen = ruta_guardar;
                        oProducto.NombreImagen = nombre_imagen;
                        bool rspta = new CN_Producto().GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else {

                        mensaje = "Se guardaró el producto pero hubo problemas con la carga de la imagen";
                    }

                
                }
            }




            return Json(new { operacionExitosa = operacion_exitosa,idGenerado = oProducto.IdProducto, mensaje = mensaje  }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ImagenProducto(int id) {

            bool conversion;
            Producto oproducto = new CN_Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();

            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oproducto.RutaImagen,oproducto.NombreImagen), out conversion );


            return Json(new
            {
                conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(oproducto.NombreImagen)

            },
             JsonRequestBehavior.AllowGet
            );

            //return Json(new { ruta = oproducto.RutaImagen }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Producto().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        // ++++++++++++++++ UBICACION ++++++++++++++++++++
        #region UBICACION;
        [HttpPost]
        public JsonResult ObtenerProvinciaArg()
        {

            List<Provincia> oLista = new List<Provincia>();

            oLista = new CN_Ubicacion().ObtenerProvinciaArg();

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerLocalidadArg(string IdProvincia)
        {

            List<Localidad> oLista = new List<Localidad>();

            oLista = new CN_Ubicacion().ObtenerLocalidadArg(IdProvincia);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }
        #endregion;

        // ++++++++++++++++ SEDE ++++++++++++++++++++
        #region SEDE;
        [HttpPost]
        public JsonResult ObtenerSede(string IdLocalidad)
        {

            List<Sede> oLista = new List<Sede>();

            oLista = new CN_Sede().ObtenerSede(IdLocalidad);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerTodasLasSedes()
        {

            List<Sede> oLista = new List<Sede>();

            oLista = new CN_Sede().ObtenerTodasLasSedes();

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        // ++++++++++++++++ DEPOSITO ++++++++++++++++++++
        #region DEPOSITO;
        [HttpGet]
        public JsonResult ListarMarca2()
        {
            List<ReporteDeposito> oLista = new List<ReporteDeposito>();

            oLista = new CN_Deposito().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GuardarDeposito(Deposito objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdDeposito == 0)
            {
                resultado = new CN_Deposito().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Deposito().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarDeposito(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Deposito().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerDeposito(string IdSede)
        {

            List<Deposito> oLista = new List<Deposito>();

            oLista = new CN_Deposito().ObtenerDeposito(IdSede);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AbrirDeposito(ReporteDeposito objeto)
        {
            //TempData["NomDeposito"] = objeto.Nombre;
            //TempData["NomSede"] = objeto.Area;
            return Json(new { Status = true, Link = "/Mantenedor/ArtXDeposito/?idDeposito="+objeto.IdDeposito+ "&NomDeposito="+objeto.Nombre+"&NomSede="+objeto.Area }, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public JsonResult ObtenerDepositoTransferir(string IdDeposito)
        {

            List<Deposito> oLista = new List<Deposito>();

            oLista = new CN_Deposito().ObtenerDepositoTransferir(IdDeposito);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TransferirArticulo(int idartxdep, int iddepositoorigen, int iddepositodestino , int cantidad)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Deposito().TransferirArticulo(idartxdep, iddepositoorigen, iddepositodestino, cantidad, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion;

        // ++++++++++++++++ ARTXDEPOSITO ++++++++++++++++++++
        #region ARTXDEPOSITO;
        [HttpPost]
        public JsonResult GuardarArtXDeposito(ArtXDeposito objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdArtXDeposito == 0)
            {

                resultado = new CN_ArtXDeposito().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_ArtXDeposito().Editar(objeto, out mensaje);

            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProductoXDeposito(int idart, int iddep)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Producto().EliminarProductoXDeposito(idart, iddep, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion;
    }
}



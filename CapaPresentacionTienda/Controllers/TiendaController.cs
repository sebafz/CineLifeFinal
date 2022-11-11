using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;

using System.IO;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;

using CapaPresentacionTienda.Filter;
using CapaEntidad.Paypal;
using System.Text;

namespace CapaPresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MetodoDePago()
        {
            return View();
        }

        public ActionResult Perfil()
        {
            return View();
        }

        public ActionResult Butacas()
        {
            return View();
        }

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

        public ActionResult DetalleProducto(int idpelicula = 0)
        {

            Pelicula oPelicula = new Pelicula();
            bool conversion;


            oPelicula = new CN_Pelicula().Listar().Where(p => p.IdPelicula == idpelicula).FirstOrDefault();


            if (oPelicula != null) {
                oPelicula.Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oPelicula.RutaImagen, oPelicula.NombreImagen), out conversion);
                oPelicula.Extension = Path.GetExtension(oPelicula.NombreImagen);
            }

            return View(oPelicula);
        }
        [HttpPost]
        public async Task<JsonResult> AbrirButacas(int idfuncion, int cantidad)
        {
            Sala oSala = new Sala();
            oSala = new CN_Sala().ListarXFuncion(idfuncion).FirstOrDefault();

            return Json(new { Status = true, Link = "/Tienda/Butacas/?IdFuncion=" + idfuncion + "&Cantidad=" + cantidad + "&Sala=" + oSala.Descripcion}, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public JsonResult VistaPerfil()
        {
            Cliente cliente = (Cliente)Session["Cliente"];

            Cliente objeto = new CN_Cliente().VerPerfil(cliente.IdCliente);

            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerSedesActivas()
        {

            List<Sede> oLista = new List<Sede>();

            oLista = new CN_Sede().ObtenerActivas();

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerFechas(int IdPelicula)
        {
            List<string> oLista = new List<string>();

            oLista = new CN_Funcion().ObtenerFechas(IdPelicula);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerButacasOcupadas(int IdFuncion)
        {
            List<string> oLista = new List<string>();

            oLista = new CN_Funcion().ObtenerButacasOcupadas(IdFuncion);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerHorasFuncion(int IdPelicula, string Fecha, int IdIdioma)
        {
            List<Funcion> oLista = new List<Funcion>();

            oLista = new CN_Funcion().ObtenerHoras(IdPelicula, Fecha, IdIdioma);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerFuncion(int IdFuncion)
        {
            List<Funcion> oLista = new List<Funcion>();

            oLista = new CN_Funcion().ObtenerFuncion(IdFuncion);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerIdiomasXFuncion(int IdPelicula, string Fecha)
        {

            List<Idioma> oLista = new List<Idioma>();

            oLista = new CN_Idioma().ListarXFuncion(IdPelicula, Fecha);

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }
        
        [HttpPost]
        public JsonResult ListarIdiomas(int IdPelicula, string Fecha)
        {

            List<Idioma> oLista = new List<Idioma>();

            oLista = new CN_Idioma().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ListarGeneros() {
            List<Genero> lista = new List<Genero>();
            lista = new CN_Genero().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarClasificaciones()
        {
            List<Clasificacion> lista = new List<Clasificacion>();
            lista = new CN_Clasificacion().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarPeliculasTienda(int idgenero, int idclasificacion, int nroPagina) {
            List<Pelicula> lista = new List<Pelicula>();

            bool conversion;

            int _TotalRegistros;
            int _TotalPaginas;

            lista = new CN_Pelicula().ObtenerPeliculasTienda(idgenero, idclasificacion, nroPagina, 8, out _TotalRegistros, out _TotalPaginas).Select(p => new Pelicula()
            {
                IdPelicula = p.IdPelicula,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Calificacion=p.Calificacion,
                oClasificacion=p.oClasificacion,
                oGenero = p.oGenero,
                Precio = p.Precio,
                RutaImagen = p.RutaImagen,
                Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.RutaImagen, p.NombreImagen), out conversion),
                Extension = Path.GetExtension(p.NombreImagen),
                Activo=p.Activo
            }).ToList();


            var jsonresult = Json(new { data = lista, totalRegistros = _TotalRegistros, totalPaginas = _TotalPaginas }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;

        }



        [HttpPost]
        public JsonResult ListarProductoActivo(int idcategoria, int idmarca, int nroPagina)
        {
            List<Producto> lista = new List<Producto>();

            bool conversion;

            int _TotalRegistros;
            int _TotalPaginas;

            lista = new CN_Producto().ObtenerProductosActivos(idmarca, idcategoria, nroPagina, 8, out _TotalRegistros, out _TotalPaginas).Select(p => new Producto()
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarca = p.oMarca,
                oCategoria = p.oCategoria,
                Precio = p.Precio,
                Stock = p.Stock,
                RutaImagen = p.RutaImagen,
                Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.RutaImagen, p.NombreImagen), out conversion),
                Extension = Path.GetExtension(p.NombreImagen),
                Activo = p.Activo
            }).ToList();


            var jsonresult = Json(new { data = lista, totalRegistros = _TotalRegistros, totalPaginas = _TotalPaginas }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;


        }

        [HttpPost]
        public JsonResult AgregarCarrito(int idproducto) {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            bool existe = new CN_Carrito().ExisteCarrito(idcliente, idproducto);

            bool respuesta = false;

            string mensaje = string.Empty;

            if (existe)
            {
                mensaje = "El producto ya existe en el carrito";

            }
            else {

                respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);
            }

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult GuardarCliente(Cliente objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCliente == 0)
            {

                resultado = new CN_Cliente().RegistrarEditar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Cliente().Editar(objeto, out mensaje);

            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult CantidadEnCarrito() {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;
            int cantidad = new CN_Carrito().CantidadEnCarrito(idcliente);
            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ListarProductosCarrito() {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            List<Carrito> oLista = new List<Carrito>();

            bool conversion;

            oLista = new CN_Carrito().ListarProducto(idcliente).Select(oc => new Carrito()
            {
                oProducto = new Producto()
                {
                    IdProducto = oc.oProducto.IdProducto,
                    Nombre = oc.oProducto.Nombre,
                    oMarca = oc.oProducto.oMarca,
                    Precio = oc.oProducto.Precio,
                    RutaImagen = oc.oProducto.RutaImagen,
                    Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oc.oProducto.RutaImagen, oc.oProducto.NombreImagen), out conversion),
                    Extension = Path.GetExtension(oc.oProducto.NombreImagen)

                },
                Cantidad = oc.Cantidad
            }).ToList();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult OperacionCarrito(int idproducto, bool sumar)
        {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;


            bool respuesta = false;

            string mensaje = string.Empty;


            respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);


            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult EliminarCarrito(int idproducto) {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = new CN_Carrito().EliminarCarrito(idcliente, idproducto);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ObtenerDepartamento() {

            List<Departamento> oLista = new List<Departamento>();

            oLista = new CN_Ubicacion().ObtenerDepartamento();

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerProvincia(string IdDepartamento)
        {

            List<Provincia> oLista = new List<Provincia>();

            oLista = new CN_Ubicacion().ObtenerProvincia(IdDepartamento);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ObtenerDistrito(string IdDepartamento, string IdProvincia)
        {

            List<Distrito> oLista = new List<Distrito>();

            oLista = new CN_Ubicacion().ObtenerDistrito(IdDepartamento, IdProvincia);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        public ActionResult Carrito() {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> EnviarPago(int IdFuncion, List<Butaca> Butacas) {

            Funcion oFuncion = new Funcion();
            oFuncion = new CN_Funcion().Listar().Where(p => p.IdFuncion == IdFuncion).FirstOrDefault();

            Session["Funcion"] = oFuncion;
            Session["Butacas"] = Butacas;
            return Json(new { Status = true, Link = "/Tienda/MetodoDePago" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ProcesarPago()
        {
            Random random = new Random();
            var numero = random.Next(100, 1000);

            object resultado;
            string mensaje = string.Empty;

            List<Butaca> oLista = (List<Butaca>)Session["Butacas"];
            Funcion oFuncion = (Funcion)Session["Funcion"];

            resultado = new CN_Comprobante().RegistrarVentaPelicula(oFuncion, ((Cliente)Session["Cliente"]).IdCliente, oLista, numero, out mensaje);


            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;
            string correo = ((Cliente)Session["Cliente"]).Correo;
            string nombreCliente = ((Cliente)Session["Cliente"]).Nombres + " " + ((Cliente)Session["Cliente"]).Apellidos;

            //Envío de mail de confirmación

            //string asunto = "CineLife: Información de tu compra";

            //StringBuilder mensaje_correo = new StringBuilder();

            //mensaje_correo.Append(
            //    "<h3 style='background-color: #f5f6fa;padding: 15px;margin: 0 5px 0;color: #004aad;border-radius: 15px;'>¡Gracias por tu compra!</h3>" +
            //    "<h4 style='padding-left: 10px; color:black'>"+nombreCliente+", te dejamos tu resumen de compra.</h4>" +
            //    "<div style='margin: 10px 8px'>Fecha: " + DateTime.Now.ToString("dd-MM-yyyy") +"</div>" +
            //    "<div style='margin: 10px 8px;'>Nro. de transacción: code" + numTrans + "</div>" +
            //    "<table  style='width: 100 %; border: 1px solid #999;text-align: left;border-collapse: collapse;margin: 0 0 1em 0;caption-side: top;'>" +
            //    "<tbody> <tr> <th style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%; background-color: #f5f6fa;'>Nro.</th>" +
            //    "<th style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%; background-color: #f5f6fa;'>Producto</th>" +
            //    "<th style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%; background-color: #f5f6fa;'>Precio</th>" +
            //    "<th style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%; background-color: #f5f6fa;'>Cantidad</th>" +
            //    "<th style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%; background-color: #f5f6fa;'>subtotal</th> </tr> ");

            //int cont = 0;
            //foreach (Butaca but in oLista)
            //{
            //    cont++;
            //    mensaje_correo.Append("<tr>" +
            //    "<td style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%;'>" + cont + "</td>" +
            //    "<td style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%;'>{NomProd" + cont + "}</td>" +
            //    "<td style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%;'>$ {Precio" + cont + "}</td>" +
            //    "<td style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%;'>{Cantidad" + cont + "}</td>" +
            //    "<td style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%;'>$ "+ carrito.oProducto.Precio * carrito.Cantidad + "</td>" +
            //    "</tr>");

            //    mensaje_correo.Replace("{NomProd" + cont + "}", carrito.oProducto.Nombre);
            //    mensaje_correo.Replace("{Precio" + cont + "}", carrito.oProducto.Precio.ToString());
            //    mensaje_correo.Replace("{Cantidad" + cont + "}", carrito.Cantidad.ToString());
            //}
            //mensaje_correo.Append("<tr> <td></td> <td><strong>Total</strong></td> <td></td> <td></td> <td>$ " + total+"</td></tbody></table>");
            //mensaje_correo.Append("<div style='margin: 22px 8px'>Gracias, Fun House</div>");
            //bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo.ToString());

            if (mensaje=="")
            {
                return Json(new { Status = true, Link = "/Tienda/PagoEfectuado?idTransaccion=" + numero + "&status=true" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Status = true, Link = "/Tienda/PagoEfectuado?idTransaccion=" + numero + "&status=false" }, JsonRequestBehavior.AllowGet);
            }


        }

        //[ValidarSession]
        //[Authorize]

        //VIEW DEL PAGO:
        public async Task<ActionResult> PagoEfectuado() {

            ViewData["Status"] = Convert.ToBoolean(Request.QueryString["status"]);
            ViewData["IdTransaccion"]= Request.QueryString["idTransaccion"];

            return View();
        }

        //[ValidarSession]
        //[Authorize]
        public ActionResult MisCompras()
        {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            List<DetalleBoleto> oLista = new List<DetalleBoleto>();

            bool conversion;

            oLista = new CN_Venta().ListarCompras(idcliente).Select(db => new DetalleBoleto()
            {
                Precio = db.Precio,
                oFuncion = new Funcion()
                {
                    Fecha = db.oFuncion.Fecha,
                    Hora=db.oFuncion.Hora,
                    oIdioma = new Idioma()
                    {
                        Descripcion=db.oFuncion.oIdioma.Descripcion
                    },
                    oSala=new Sala()
                    {
                        Descripcion=db.oFuncion.oSala.Descripcion
                    },
                    oPelicula = new Pelicula()
                    {
                        Nombre=db.oFuncion.oPelicula.Nombre,
                        RutaImagen = db.oFuncion.oPelicula.RutaImagen,
                        Base64 = CN_Recursos.ConvertirBase64(Path.Combine(db.oFuncion.oPelicula.RutaImagen, db.oFuncion.oPelicula.NombreImagen), out conversion),
                        Extension = Path.GetExtension(db.oFuncion.oPelicula.NombreImagen)
                    }
                },
                oComprobante = new Comprobante()
                {
                    Numero=db.oComprobante.Numero,
                    Fecha=db.oComprobante.Fecha
                },
                oButaca=new Butaca()
                {
                    Fila=db.oButaca.Fila,
                    Numero=db.oButaca.Numero
                }
            }).ToList();

            return View(oLista);

        }
    }
}
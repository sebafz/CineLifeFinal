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

        public ActionResult DetalleProducto(int idproducto = 0)
        {

            Producto oProducto = new Producto();
            bool conversion;


            oProducto = new CN_Producto().Listar().Where(p => p.IdProducto == idproducto).FirstOrDefault();


            if (oProducto != null) {
                oProducto.Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen, oProducto.NombreImagen), out conversion);
                oProducto.Extension = Path.GetExtension(oProducto.NombreImagen);
            }

            return View(oProducto);
        }

        [HttpGet]
        public JsonResult VistaPerfil()
        {
            Cliente cliente = (Cliente)Session["Cliente"];

            Cliente objeto = new CN_Cliente().VerPerfil(cliente.IdCliente);

            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListaCategorias() {
            List<Categoria> lista = new List<Categoria>();
            lista = new CN_Categoria().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListaCategoriasActivas()
        {
            List<Categoria> lista = new List<Categoria>();
            lista = new CN_Categoria().Listar();

            List<Categoria> listaActivas = new List<Categoria>();
            foreach (Categoria cat in lista)
            {
                if (cat.Activo)
                {
                    listaActivas.Add(cat);
                }
            }
            return Json(new { data = listaActivas }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarMarcaporCategoria(int idcategoria)
        {
            List<Marca> lista = new List<Marca>();
            lista = new CN_Marca().ListarMarcaporCategoria(idcategoria);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarProducto(int idcategoria, int idmarca, int nroPagina) {
            List<Producto> lista = new List<Producto>();

            bool conversion;

            int _TotalRegistros;
            int _TotalPaginas;

            lista = new CN_Producto().ObtenerProductos(idmarca, idcategoria, nroPagina, 8, out _TotalRegistros, out _TotalPaginas).Select(p => new Producto()
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
        public async Task<JsonResult> EnviarPago(List<Carrito> oListaCarrito, Venta oVenta) {
            Session["Lista1"] = oListaCarrito;
            Session["Venta1"] = oVenta;
            return Json(new { Status = true, Link = "/Tienda/MetodoDePago" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> ProcesarPago() {

            List<Carrito> oListaCarrito = (List<Carrito>) Session["Lista1"];
            Venta oVenta = (Venta) Session["Venta1"];

            decimal total = 0;
            DataTable detalle_venta = new DataTable();
            detalle_venta.Locale = new CultureInfo("es-PE");
            detalle_venta.Columns.Add("IdProducto", typeof(string));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("Total", typeof(decimal));

            foreach (Carrito oCarrito in oListaCarrito)
            {
                decimal subtotal = Convert.ToDecimal(oCarrito.Cantidad.ToString()) * oCarrito.oProducto.Precio;

                total += subtotal;

                detalle_venta.Rows.Add(new object[] {
                    oCarrito.oProducto.IdProducto,
                    oCarrito.Cantidad,
                    subtotal
                });
            }
            oVenta.MontoTotal = total;
            oVenta.IdCliente = ((Cliente)Session["Cliente"]).IdCliente;
            string correo = ((Cliente)Session["Cliente"]).Correo;
            string nombreCliente= ((Cliente)Session["Cliente"]).Nombres+" "+((Cliente)Session["Cliente"]).Apellidos;

            Random random = new Random();
            int numTrans = random.Next(1000, 10000);

            TempData["Venta"] = oVenta;
            TempData["DetalleVenta"] = detalle_venta;

            //Envío de mail de confirmación

            string asunto = "Fun House: Comprobante de compra";

            StringBuilder mensaje_correo = new StringBuilder();

            mensaje_correo.Append(
                "<h3 style='background-color: #f5f6fa;padding: 15px;margin: 0 5px 0;color: #004aad;border-radius: 15px;'>¡Gracias por tu compra!</h3>" +
                "<h4 style='padding-left: 10px; color:black'>"+nombreCliente+", te dejamos tu resumen de compra.</h4>" +
                "<div style='margin: 10px 8px'>Fecha: " + DateTime.Now.ToString("dd-MM-yyyy") +"</div>" +
                "<div style='margin: 10px 8px;'>Nro. de transacción: code" + numTrans + "</div>" +
                "<div style='margin: 10px 8px 15px;'>Dirección: " + oVenta.Direccion.ToString() +"</div>" +
                "<table  style='width: 100 %; border: 1px solid #999;text-align: left;border-collapse: collapse;margin: 0 0 1em 0;caption-side: top;'>" +
                "<tbody> <tr> <th style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%; background-color: #f5f6fa;'>Nro.</th>" +
                "<th style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%; background-color: #f5f6fa;'>Producto</th>" +
                "<th style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%; background-color: #f5f6fa;'>Precio</th>" +
                "<th style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%; background-color: #f5f6fa;'>Cantidad</th>" +
                "<th style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%; background-color: #f5f6fa;'>subtotal</th> </tr> ");

            int cont = 0;
            foreach (Carrito carrito in oListaCarrito)
            {
                cont++;
                mensaje_correo.Append("<tr>" +
                "<td style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%;'>" + cont + "</td>" +
                "<td style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%;'>{NomProd" + cont + "}</td>" +
                "<td style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%;'>$ {Precio" + cont + "}</td>" +
                "<td style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%;'>{Cantidad" + cont + "}</td>" +
                "<td style='padding: 0.3em; border-bottom: 1px solid #999;width: 25%;'>$ "+ carrito.oProducto.Precio * carrito.Cantidad + "</td>" +
                "</tr>");

                mensaje_correo.Replace("{NomProd" + cont + "}", carrito.oProducto.Nombre);
                mensaje_correo.Replace("{Precio" + cont + "}", carrito.oProducto.Precio.ToString());
                mensaje_correo.Replace("{Cantidad" + cont + "}", carrito.Cantidad.ToString());
            }
            mensaje_correo.Append("<tr> <td></td> <td><strong>Total</strong></td> <td></td> <td></td> <td>$ " + total+"</td></tbody></table>");
            mensaje_correo.Append("<div style='margin: 22px 8px'>Gracias, Fun House</div>");
            bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo.ToString());

            return Json(new { Status = true, Link = "/Tienda/PagoEfectuado?idTransaccion=code" + numTrans + "&status=true" }, JsonRequestBehavior.AllowGet);


        }

        //[ValidarSession]
        //[Authorize]
        public async Task<ActionResult> PagoEfectuado() {

            string idtransaccion = Request.QueryString["idTransaccion"];
            bool status = Convert.ToBoolean(Request.QueryString["status"]);

            ViewData["Status"] = status;

            if (status)
            {
                Venta oVenta = (Venta)TempData["Venta"];

                DataTable detalle_venta = (DataTable)TempData["DetalleVenta"];

                oVenta.IdTransaccion = idtransaccion;

                string mensaje = string.Empty;

                bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);

                ViewData["IdTransaccion"] = oVenta.IdTransaccion;
            }
            return View();
        }

        //[ValidarSession]
        //[Authorize]
        public ActionResult MisCompras()
        {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            List<DetalleVenta> oLista = new List<DetalleVenta>();

            bool conversion;

            oLista = new CN_Venta().ListarCompras(idcliente).Select(oc => new DetalleVenta()
            {
                oProducto = new Producto()
                {
                    Nombre = oc.oProducto.Nombre,
                    Precio = oc.oProducto.Precio,
                    RutaImagen = oc.oProducto.RutaImagen,
                    Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oc.oProducto.RutaImagen, oc.oProducto.NombreImagen), out conversion),
                    Extension = Path.GetExtension(oc.oProducto.NombreImagen)

                },
                Cantidad = oc.Cantidad,
                Total = oc.Total,
                IdTransaccion = oc.IdTransaccion
            }).ToList();

            return View(oLista);

        }
    }
}
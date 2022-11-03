using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



using CapaEntidad;
using CapaNegocio;
using ClosedXML.Excel;

namespace CapaPresentacionAdmin.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Chart()
        {
            return View();
        }
        public ActionResult Usuarios()
        {
            return View();
        }

        public ActionResult Proveedores()
        {
            return View();
        }

        public ActionResult Clientes()
        {
            return View();
        }
        public ActionResult Sedes()
        {
            return View();
        }
        public ActionResult Depositos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {


            List<Usuario> oLista = new List<Usuario>();

            oLista = new CN_Usuarios().Listar();


            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ListarProveedores()
        {


            List<Proveedor> oLista = new List<Proveedor>();

            oLista = new CN_Proveedores().Listar();


            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ListarClientes()
        {


            List<Cliente> oLista = new List<Cliente>();

            oLista = new CN_Cliente().Listar();


            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ListarSedes()
        {


            List<Sede> oLista = new List<Sede>();

            oLista = new CN_Sede().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdUsuario == 0)
            {

                resultado = new CN_Usuarios().Registrar(objeto, out mensaje);
            }
            else {
                resultado = new CN_Usuarios().Editar(objeto, out mensaje);
            
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProveedor(Proveedor objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdProveedor == 0)
            {

                resultado = new CN_Proveedores().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Proveedores().Editar(objeto, out mensaje);

            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult GuardarSede(Sede objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdSede == 0)
            {

                resultado = new CN_Sede().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Sede().Editar(objeto, out mensaje);

            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id) {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuarios().Eliminar(id,out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProveedor(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Proveedores().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarCliente(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Cliente().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarSede(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Sede().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListaReporte(string fechainicio,string fechafin,string idtransaccion)
        {
            List<Reporte> oLista = new List<Reporte>();

            oLista = new CN_Reporte().Ventas(fechainicio,fechafin,idtransaccion);

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public JsonResult VistaDashBoard() {
            DashBoard objeto = new CN_Reporte().VerDashBoard();

            return Json(new { resultado = objeto}, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public FileResult ExportarVenta(string fechainicio, string fechafin, string idtransaccion) {

            List<Reporte> oLista = new List<Reporte>();
            oLista = new CN_Reporte().Ventas(fechainicio, fechafin, idtransaccion);

            DataTable dt = new DataTable();

            dt.Locale = new System.Globalization.CultureInfo("es-PE");
            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("IdTransaccion", typeof(string));


            foreach (Reporte rp in oLista) {
                dt.Rows.Add(new object[] {
                    rp.FechaVenta,
                    rp.Cliente,
                    rp.Producto,
                    rp.Precio,
                    rp.Cantidad,
                    rp.Total,
                    rp.IdTransaccion
                });
            
            }

            dt.TableName = "Datos";

            using (XLWorkbook wb = new XLWorkbook()) {

                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");
                
                }
            }



        }



    }
}
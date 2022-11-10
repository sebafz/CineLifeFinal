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
    public class ComprasController : Controller
    {
        // GET: Compras
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FacturasCompra()
        {
            return View();
        }
        public ActionResult NotasCompra()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarComprobantesCompra(int tipo)
        {
            List<Comprobante> oLista = new List<Comprobante>();
            oLista = new CN_Comprobante().ListarCompra(tipo);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarNotasCompra()
        {
            List<Comprobante> oLista = new List<Comprobante>();
            oLista = new CN_Comprobante().ListarNotasCompra();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RegistrarComprobanteCompra(Comprobante comprobante, List<Producto> productos)
        {
            object resultado;
            string mensaje = string.Empty;

            resultado = new CN_Comprobante().RegistrarCompra(comprobante, productos, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public JsonResult VincularMovimientos(int id, int tipo)
        //{
        //    string mensaje;

        //    bool respuesta = new CN_Movimiento().Vincular(id, tipo, out mensaje);

        //    return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        //}
    }
}
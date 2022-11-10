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
    public class VentasController : Controller
    {
        // GET: Ventas
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FacturasVenta()
        {
            return View();
        }
        public ActionResult NotasVenta()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarComprobantesVenta(int tipo)
        {
            List<Comprobante> oLista = new List<Comprobante>();
            oLista = new CN_Comprobante().ListarVenta(tipo);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarComprobanteVenta(Comprobante comprobante, List<Producto> productos)
        {
            object resultado;
            string mensaje = string.Empty;

            resultado = new CN_Comprobante().RegistrarVenta(comprobante, productos, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarNotasVenta()
        {
            List<Comprobante> oLista = new List<Comprobante>();
            oLista = new CN_Comprobante().ListarNotasVenta();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
    }
}
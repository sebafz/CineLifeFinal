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
        public JsonResult ListarComprobantes(int tipo)
        {
            List<Comprobante> oLista = new List<Comprobante>();
            oLista = new CN_Comprobante().Listar(tipo, 0);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
    }
}
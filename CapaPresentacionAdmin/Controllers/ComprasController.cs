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
        public JsonResult ListarComprobantes(int tipo)
        {
            List<Comprobante> oLista = new List<Comprobante>();
            oLista = new CN_Comprobante().Listar(tipo, 1);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
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
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
    public class CineController : Controller
    {
        // GET: Cine
        public ActionResult Index()
        {
            return View();
        }
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
    }
}
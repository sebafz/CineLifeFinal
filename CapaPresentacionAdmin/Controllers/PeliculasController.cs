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


        #region Funciones
        public JsonResult ListarFunciones()
        {
            List<Funcion> oLista = new List<Funcion>();
            oLista = new CN_Funcion().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}
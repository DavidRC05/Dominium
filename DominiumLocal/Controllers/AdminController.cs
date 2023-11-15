using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DominiumLocal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Propiedades()
        {
            return View();
        }
        public ActionResult Usuarios()
        {
            return View();
        }
        public ActionResult Vendedores()
        {
            return View();
        }
        public ActionResult AgregarPropiedad()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
    }
}
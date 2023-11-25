using DominiumLocal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DominiumLocal.Controllers
{
    public class HomeController : Controller
    {
        propiedadModel propiedadModel = new propiedadModel();
        public ActionResult Index()
        { 
            return View();
        }


        public ActionResult Propiedades()
        {
            var datos = propiedadModel.VerPropiedades();
            return View(datos);
        }

    }
}
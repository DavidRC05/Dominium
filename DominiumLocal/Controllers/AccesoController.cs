using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DominiumLocal.Models;
using DominiumLocal.Entity;
using System.Data.SqlClient;
using System.Data;

namespace DominiumLocal.Controllers
{
    public class AccesoController : Controller
    {
        userModel userModel = new userModel();

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CRegister(usersEntity entidad)
        {
            var resp = userModel.CRegister(entidad);

            if (resp == "OK")
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha registrado su información";
                return View();
            }
        }

    }
}
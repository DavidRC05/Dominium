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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(usersEntity entidad)
        {
            var resp = userModel.Login(entidad);

            if (resp != null)
            {
                Session["Nombre"] = resp.FirstName;
                Session["ConUsuario"] = resp.Password;
                Session["RoleID"] = resp.Rol;
                if (resp.FirstName == null)
                {
                    return RedirectToAction("Login", "Acceso");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.MensajeUsuario = "Compruebe la información de sus credenciales";
                return View();
            }
        }

        [HttpGet]
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
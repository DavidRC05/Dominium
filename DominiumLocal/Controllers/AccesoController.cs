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
                Session["Apellido"] = resp.LastName;
                Session["Email"] = resp.Email;
                Session["Imagen"] = resp.ProfilePicture;
                Session["Descripcion"] = resp.Description;
                Session["ConUsuario"] = resp.Password;
                Session["Rol"] = resp.Rol;
                Session["UserID"] = resp.UserID;
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
        public ActionResult Register(usersEntity entidad)
        {
            var resp = userModel.Register(entidad);

            if (resp == "OK")
            {
                TempData["MensajeExito"] = "Se ha registrado con éxito.";
                return RedirectToAction("Login", "Acceso");
            }
            else if (resp == "EmailYaExiste")
            {
                ViewBag.MensajeUsuario = "El email ya está registrado.";
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha registrado su información";
            }
            return View();
        }



        [HttpGet]
        public ActionResult RecuperarCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarCuenta(usersEntity entidad)
        {
            var resp = userModel.RecuperarCuenta(entidad);

            if (resp == "OK")
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha enviado el correo con su información";
                return View();
            }
        }
    }
}
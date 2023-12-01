using DominiumLocal.Entity;
using DominiumLocal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DominiumLocal.Controllers
{
    public class AdminController : Controller
    {
        propiedadModel propiedadModel = new propiedadModel();
        userModel userModel = new userModel();

        // GET: Admin
        public ActionResult Propiedades()
        {
            int idUsuarioEnSesion = Convert.ToInt32(Session["UserID"]);
            var datos = propiedadModel.ConsultarPropiedades(idUsuarioEnSesion);
            return View(datos);
        }


        [HttpPost]
        public ActionResult AgregarPropiedad(HttpPostedFileBase ImgPropiedad, propiedadEntity entidad)
        {
            entidad.Imagen = string.Empty;
            int idUsuarioEnSesion = Convert.ToInt32(Session["UserID"]);
            entidad.IDVendedor = idUsuarioEnSesion;

            long PropiedadID = propiedadModel.AgregarPropiedad(entidad, idUsuarioEnSesion);

            if (PropiedadID > 0)
            {
                string extension = Path.GetExtension(Path.GetFileName(ImgPropiedad.FileName));
                string ruta = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + PropiedadID + extension;
                ImgPropiedad.SaveAs(ruta);

                entidad.Imagen = "/Images/" + PropiedadID + extension;
                entidad.PropiedadID = PropiedadID;

                propiedadModel.ActualizarRutaImagen(entidad);

                return RedirectToAction("Propiedades", "Admin");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha registrado la propiedad";
                return View();
            }
        }

        public ActionResult Perfil()
        {
            long iduser = Convert.ToInt64(Session["UserID"]);
            int idUsuarioEnSesion = Convert.ToInt32(Session["UserID"]);
            var usuario = userModel.ConsultaUsuario(iduser);

            // Obtener propiedades del usuario (ajusta según tu lógica)
            var propiedades = propiedadModel.ConsultarPropiedades(idUsuarioEnSesion).Take(3);

            ViewBag.Usuario = usuario;
            ViewBag.Propiedades = propiedades;

            return View(usuario);
        }


        public ActionResult EditarPerfil()
        {
            long iduser = Convert.ToInt64(Session["UserID"]);
            var datos = userModel.ConsultaUsuario(iduser);
            return View(datos);
        }


        [HttpPost]
        public ActionResult ActualizarPerfil(HttpPostedFileBase ImgPerfil, usersEntity entidad)
        {
            string respuesta = userModel.ActualizarPerfil(entidad);

            if (respuesta == "OK")
            {
                if (ImgPerfil != null)
                {
                    string extension = Path.GetExtension(Path.GetFileName(ImgPerfil.FileName));
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "profiles\\" + entidad.UserID + extension;
                    ImgPerfil.SaveAs(ruta);

                    entidad.ProfilePicture = "/profiles/" + entidad.UserID + extension;
                    entidad.UserID = entidad.UserID;

                    userModel.ActualizarRutaImagenPerfil(entidad);
                }


                return RedirectToAction("Perfil", "Admin");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido editar el perfil";
                return View();
            }
        }

        public ActionResult Vendedores()
        {
            return View();
        }

        //Seccion de Propiedades
        [HttpGet]
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
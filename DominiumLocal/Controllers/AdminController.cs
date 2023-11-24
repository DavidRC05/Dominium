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

        //Seccion de Propiedades
        [HttpGet]
        public ActionResult AgregarPropiedad()
        {
            return View();
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


        public ActionResult Menu()
        {
            return View();
        }
    }
}
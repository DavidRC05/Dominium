using DominiumLocal.Entity;
using DominiumLocal.Models;
using PagedList;
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
        visitaModel visitaModel = new visitaModel();


        public ActionResult Propiedades(int? page, int? pageSize)
        {
            int pageNumber = page ?? 1;
            int pageSizeNumber = pageSize ?? 4; // ajusta según tus necesidades

            int idUsuarioEnSesion = Convert.ToInt32(Session["UserID"]);
            var datos = propiedadModel.ConsultarPropiedades(idUsuarioEnSesion);

            var propiedadesPaginadas = datos.ToPagedList(pageNumber, pageSizeNumber);

            return View(propiedadesPaginadas);
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

        [HttpGet]
        public ActionResult ActualizarPropiedad(long q)
        {
            var datos = propiedadModel.ConsultaPropiedad(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult ActualizarPropiedad(HttpPostedFileBase ImgPropiedad, propiedadEntity entidad)
        {
            string respuesta = propiedadModel.ActualizarPropiedad(entidad);

            if (respuesta == "OK")
            {
                if (ImgPropiedad != null)
                {
                    string extension = Path.GetExtension(Path.GetFileName(ImgPropiedad.FileName));
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + entidad.PropiedadID + extension;
                    ImgPropiedad.SaveAs(ruta);

                    entidad.Imagen = "/Images/" + entidad.PropiedadID + extension;
                    entidad.PropiedadID = entidad.PropiedadID;

                    propiedadModel.ActualizarRutaImagen(entidad);
                }
                return RedirectToAction("Propiedades", "Admin");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido actualizar la información del producto";
                return View();
            }
        }

        [HttpGet]
        public ActionResult EliminarPropiedad(long q)
        {
            var entidad = new propiedadEntity();
            entidad.PropiedadID = q;

            string respuesta = propiedadModel.EliminarPropiedad(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("Propiedades", "Admin");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido cambiar el estado del producto";
                return View();
            }
        }


        public ActionResult Perfil(bool mostrarTodas = false)
        {
            int iduser = Convert.ToInt32(Session["UserID"]);
            var usuario = userModel.ConsultaUsuario(iduser);

            IEnumerable<propiedadEntity> propiedades;
            if (mostrarTodas)
            {
                propiedades = propiedadModel.ConsultarPropiedades(iduser);
            }
            else
            {
                propiedades = propiedadModel.ConsultarPropiedades(iduser).Take(3);
            }

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

        [HttpGet]
        public ActionResult AdministrarVisitas()
        {
            int idUsuarioEnSesion = Convert.ToInt32(Session["UserID"]);
            var eventos = visitaModel.GetVisitas(idUsuarioEnSesion);
            return View(eventos);
        }

        //Seccion de Propiedades
        [HttpGet]
        public ActionResult AgregarPropiedad()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AgendarVisita()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgendarVisita(visitaEntity entidad)
        {
            entidad.VendedorId = Convert.ToInt32(Session["UserID"]);
            var resp = visitaModel.AgendarVisita(entidad);

            if (resp > 0)
            {
                return RedirectToAction("Calendario", "Admin");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha registrado su visita";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Calendario()
        {
            int idUsuarioEnSesion = Convert.ToInt32(Session["UserID"]);
            var eventos = visitaModel.GetVisitas(idUsuarioEnSesion);
            return View(eventos);
        }



        [HttpGet]
        public ActionResult EliminarVisita(int q)
        {
            var entidad = new visitaEntity();
            entidad.Id = q;

            string respuesta = visitaModel.EliminarVisita(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("AdministrarVisitas", "Admin");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido cambiar el estado del producto";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarVisita(int q)
        {
            var datos = visitaModel.ConsultaVisita(q);
            return View(datos);
        }


        [HttpPost]
        public ActionResult ActualizarVisita(visitaEntity entidad)
        {
            string respuesta = visitaModel.ActualizarVisita(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("AdministrarVisitas", "Admin");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido actualizar la información de la visita";
                return View();
            }
        }
    }
}
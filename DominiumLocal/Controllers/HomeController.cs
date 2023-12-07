using DominiumLocal.Entity;
using DominiumLocal.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DominiumLocal.Controllers
{
    public class HomeController : Controller
    {
        propiedadModel propiedadModel = new propiedadModel();
        userModel userModel = new userModel();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Propiedades(int? page, int? pageSize, string provinciaFilter, string categoriaFilter)
        {
            int pageNumber = page ?? 1;
            int pageSizeNumber = pageSize ?? 9; // ajusta según tus necesidades

            IEnumerable<propiedadEntity> datos = propiedadModel.VerPropiedades();

            // Aplicar filtros
            if (!string.IsNullOrEmpty(provinciaFilter) && provinciaFilter != "*")
            {
                int provinciaID = int.Parse(provinciaFilter);
                datos = datos.Where(p => p.ProvinciaID == provinciaID);
            }

            if (!string.IsNullOrEmpty(categoriaFilter) && categoriaFilter != "*")
            {
                datos = datos.Where(p => p.Categoria.ToLower() == categoriaFilter.ToLower());
            }

            // Obtener el total de elementos antes de la paginación
            int totalItems = datos.Count();

            // Paginar los resultados
            var properties = datos.ToPagedList(pageNumber, pageSizeNumber);

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSizeNumber;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSizeNumber);

            return View(properties);
        }

        [HttpGet]
        public async Task<ActionResult> DetallesPropiedadId(int propiedadId)
        {
            // Obtén la información de la propiedad y el vendedor por el ID de la propiedad
            var propiedad =  propiedadModel.ObtenerPropiedadPorId(propiedadId);

            // Verifica si la propiedad existe
            if (propiedad != null)
            {
                var vendedorTask = userModel.ObtenerUsuarioPorId(propiedad.IDVendedor);
                var vendedor = await vendedorTask;

                // Verifica si el vendedor existe
                if (vendedor != null)
                {
                    // Pasa la información a la vista
                    ViewBag.Propiedad = propiedad;
                    ViewBag.Vendedor = vendedor;

                    return View("DetallesPropiedad");
                }
            }

            // Puedes manejar el caso en que los datos no estén disponibles
            return RedirectToAction("Propiedades", "Home");
        }


        [HttpGet]
        public ActionResult DetallesPropiedad()
        {
            return View();
        }


        [HttpGet]
        public ActionResult PerfilVendedor()
        {
            return View();
        }


        [HttpPost]
        public ActionResult EnviarEmail(string para, string asunto, string mensaje, string email, string contrasena)
        {
            using (MailMessage mm = new MailMessage(email, para))
            {
                mm.Subject = asunto;
                mm.Body = mensaje;

                // Configuración del cliente SMTP
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.EnableSsl = true;
                    smtp.Port = 25;

                    // Credenciales de inicio de sesión
                    NetworkCredential cred = new NetworkCredential(email, contrasena);
                    smtp.Credentials = cred;

                    // Envío del correo
                    smtp.Send(mm);

                    ViewBag.Mensaje = "Email Enviado";
                }
            }

            // Puedes redirigir a otra vista o mantener la misma, según tus necesidades
            return RedirectToAction("Propiedades", "Home");
        }



        [HttpGet]
        public async Task<ActionResult> PerfilV(int vendedorID, int propiedadID)
        {
            var propiedad = propiedadModel.ObtenerPropiedadPorId(propiedadID);
            var vendedorTask = userModel.ObtenerUsuarioPorId(vendedorID);
            var propiedadesV = propiedadModel.ConsultarPropiedades(vendedorID);
            var vendedor = await vendedorTask;

            if (vendedor != null)
            {
                ViewBag.Vendedor = vendedor;
                ViewBag.PropiedadesSubidas = propiedadesV.Take(3);

                if (propiedad != null)
                {
                    ViewBag.Propiedad = new List<propiedadEntity> { propiedad };
                }
                else
                {
                    return RedirectToAction("Propiedades", "Home");
                }
            }
            else
            {
                return RedirectToAction("Propiedades", "Home");
            }

            // Inicializa el modelo de emailEntity y lo envía a la vista
            emailEntity modelo = new emailEntity
            {
                Para = vendedor.Email, // Puedes establecer el valor predeterminado del destinatario
            };
            return View("PerfilVendedor", modelo);
        }



    }
}

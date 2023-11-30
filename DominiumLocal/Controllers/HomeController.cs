using DominiumLocal.Entity;
using DominiumLocal.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
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


        [HttpPost]
        public ActionResult VerDetalles(int propiedadId)
        {
            // Lógica para agendar la cita (puedes agregar lógica adicional según tus necesidades)

            // Redirige a la acción DetallesPropiedad con el ID de la propiedad
            return RedirectToAction("DetallesPropiedad", new { propiedadId });
        }


    }
}

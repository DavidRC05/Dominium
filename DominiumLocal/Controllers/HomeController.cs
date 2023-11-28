using DominiumLocal.Entity;
using DominiumLocal.Models;
using PagedList;
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
        public ActionResult Propiedades(int? page, int? pageSize, string provinciaFilter, string categoriaFilter)
        {
            int pageNumber = page ?? 1;
            int pageSizeNumber = pageSize ?? 3; // ajusta según tus necesidades

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





    }
}

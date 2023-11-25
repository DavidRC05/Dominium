
using DominiumAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DominiumAPI.Controllers
{
    public class AdminController : ApiController
    {
        //[HttpPost]
        //[Route("AgregarPropiedad")]
        //public long AgregarPropiedad(propiedadEntity entidad)
        //{
        //    using (var context = new DominiumEntities1())
        //    {
        //        var propiedad = new TPropiedades();
        //        propiedad.Categoria = entidad.Categoria;
        //        propiedad.Precio = entidad.Precio;
        //        propiedad.ProvinciaID = entidad.ProvinciaID;
        //        propiedad.UbicacionExacta = entidad.UbicacionExacta;
        //        propiedad.Banos = entidad.Banos;
        //        propiedad.Habitaciones = entidad.Habitaciones;
        //        propiedad.Area = entidad.Area;
        //        propiedad.Piso = entidad.Piso;
        //        propiedad.Imagen = entidad.Imagen;
        //        propiedad.Estacionamiento = entidad.Estacionamiento;
        //        propiedad.IDVendedor = entidad.IDVendedor;

        //        context.TPropiedades.Add(propiedad);
        //        context.SaveChanges();

        //        if (propiedad != null)
        //        {
        //            context.Entry(propiedad).Reference(p => p.TProvincias).Load();
        //        }

        //        return entidad.PropiedadID;
        //    }
        //}

        [HttpPost]
        [Route("AgregarPropiedad")]
        public long RegistrarProducto(TPropiedades tPropiedades)
        {
            using (var context = new DominiumEntities1())
            {
                context.TPropiedades.Add(tPropiedades);
                context.SaveChanges();
                return tPropiedades.PropiedadID;
            }
        }


        [HttpPut]
        [Route("ActualizarRutaImagen")]
        public string ActualizarRutaImagen(TPropiedades tpropiedades)
        {
            using (var context = new DominiumEntities1())
            {
                var datos = context.TPropiedades.FirstOrDefault(x => x.PropiedadID == tpropiedades.PropiedadID);

                if (datos != null)
                {
                    datos.Imagen = tpropiedades.Imagen;
                    context.SaveChanges();
                }

                return "OK";
            }
        }

        [HttpGet]
        [Route("ConsultarPropiedades/{idVendedor}")]
        public List<TPropiedades> ConsultarProductos(int idVendedor)
        {
            using (var context = new DominiumEntities1())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.TPropiedades.Where(p => p.IDVendedor == idVendedor).ToList();
            }
        }


        [HttpGet]
        [Route("VerPropiedades")]
        public List<TPropiedades> VerPropiedades()
        {
            using (var context = new DominiumEntities1())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.TPropiedades.ToList();
            }

        }

    }
}

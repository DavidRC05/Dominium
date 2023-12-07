
using DominiumAPI.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DominiumAPI.Controllers
{
    public class AdminController : ApiController
    {

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
        [Route("ActualizarPropiedad")]
        public string ActualizarProducto(TPropiedades tPropiedadad)
        {
            using (var context = new DominiumEntities1())
            {
                var datos = context.TPropiedades.Where(x => x.PropiedadID == tPropiedadad.PropiedadID).FirstOrDefault();
                datos.Categoria = tPropiedadad.Categoria;
                datos.Precio = tPropiedadad.Precio;
                datos.ProvinciaID = tPropiedadad.ProvinciaID;
                datos.Habitaciones = tPropiedadad.Habitaciones;
                datos.Banos = tPropiedadad.Banos;
                datos.Area = tPropiedadad.Area;
                datos.Piso = tPropiedadad.Piso;
                datos.Estacionamiento = tPropiedadad.Estacionamiento;
                context.SaveChanges();
                return "OK";
            }
        }

        [HttpGet]
        [Route("ConsultaPropiedad")]
        public TPropiedades ConsultaPropiedad(long q)
        {
            using (var context = new DominiumEntities1())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.TPropiedades
                        where x.PropiedadID == q
                        select x).FirstOrDefault();
            }
        }

        [HttpPut]
        [Route("EliminarPropiedad")]
        public string EliminarPropiedad(TPropiedades tPropiedad)
        {
            using (var context = new DominiumEntities1())
            {
                var propiedadAEliminar = context.TPropiedades.FirstOrDefault(x => x.PropiedadID == tPropiedad.PropiedadID);

                if (propiedadAEliminar != null)
                {
                    context.TPropiedades.Remove(propiedadAEliminar);
                    context.SaveChanges();
                    return "OK";
                }
                else
                {
                    return "La propiedad no fue encontrada";
                }
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

        [HttpPut]
        [Route("ActualizarRutaImagenPerfil")]
        public string ActualizarRutaImagenPerfil(TUsers tusers)
        {
            using (var context = new DominiumEntities1())
            {
                var datos = context.TUsers.FirstOrDefault(x => x.UserID == tusers.UserID);

                if (datos != null)
                {
                    datos.ProfilePicture = tusers.ProfilePicture;
                    context.SaveChanges();
                }

                return "OK";
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

        [HttpGet]
        [Route("ObtenerPropiedadPorId/{propiedadId}")]
        public TPropiedades ObtenerPropiedadPorId(int propiedadId)
        {
            using (var context = new DominiumEntities1())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.TPropiedades.FirstOrDefault(p => p.PropiedadID == propiedadId);
            }
        }

        [HttpGet]
        [Route("ObtenerUsuarioPorId/{userId}")]
        public async Task<TUsers> ObtenerUsuarioPorIdAsync(int userId)
        {
            using (var context = new DominiumEntities1())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return await context.TUsers.FirstOrDefaultAsync(u => u.UserID == userId);
            }
        }

        [HttpGet]
        [Route("ConsultaUsuario")]
        public TUsers ConsultaUsuario(long q)
        {
            using (var context = new DominiumEntities1())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.TUsers
                        where x.UserID == q
                        select x).FirstOrDefault();
            }
        }

        [HttpPut]
        [Route("ActualizarPerfil")]
        public string ActualizarPerfil(TUsers tUsers)
        {
            using (var context = new DominiumEntities1())
            {
                var datos = context.TUsers.Where(x => x.UserID == tUsers.UserID).FirstOrDefault();
                datos.FirstName = tUsers.FirstName;
                datos.LastName = tUsers.LastName;
                datos.Email = tUsers.Email;
                datos.PhoneNumber = tUsers.PhoneNumber;
                datos.Description = tUsers.Description;
                context.SaveChanges();
                return "OK";
            }
        }


        [HttpPost]
        [Route("AgendarVisita")]
        public long AgendarVisita(visitaEntity visita)
        {
            try
            {
                using (var context = new DominiumEntities1())
                {
                    if (visita != null && ModelState.IsValid)
                    {
                        
                        var nuevaVisita = new Visitas
                        {
                            Id = visita.Id,
                            NombreVisitante = visita.NombreVisitante,
                            PropiedadVisitada = visita.PropiedadVisitada,
                            FechaVisita = visita.FechaVisita,
                            EstadoVisita = visita.EstadoVisita,
                            IdVendedor = visita.VendedorId
                        };

                        context.Visitas.Add(nuevaVisita);
                        context.SaveChanges();

                        return nuevaVisita.Id;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        [HttpGet]
        [Route("GetVisitas/{idVendedor}")]
        public List<Visitas> GetVisitas(int idVendedor)
        {
            try
            {
                using (var context = new DominiumEntities1())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.Visitas.Where(p => p.IdVendedor == idVendedor).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("VerVisitas")]
        public List<Visitas> VerVisitas()
        {
            using (var context = new DominiumEntities1())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.Visitas.ToList();
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


        [HttpPut]
        [Route("ActualizarVisita")]
        public string ActualizarVisita(Visitas tVisitas)
        {
            using (var context = new DominiumEntities1())
            {
                var datos = context.Visitas.Where(x => x.Id == tVisitas.Id).FirstOrDefault();
                datos.NombreVisitante = tVisitas.NombreVisitante;
                datos.PropiedadVisitada = tVisitas.PropiedadVisitada;
                datos.FechaVisita = tVisitas.FechaVisita;
                datos.EstadoVisita = tVisitas.EstadoVisita;
                context.SaveChanges();
                return "OK";
            }
        }

        [HttpGet]
        [Route("ConsultaVisita")]
        public Visitas ConsultaVisita(int q)
        {
            using (var context = new DominiumEntities1())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.Visitas
                        where x.Id == q
                        select x).FirstOrDefault();
            }
        }

        [HttpPut]
        [Route("EliminarVisita")]
        public string EliminarVisita(Visitas tVisita)
        {
            using (var context = new DominiumEntities1())
            {
                var VisitaAEliminar = context.Visitas.FirstOrDefault(x => x.Id == tVisita.Id);

                if (VisitaAEliminar != null)
                {
                    context.Visitas.Remove(VisitaAEliminar);
                    context.SaveChanges();
                    return "OK";
                }
                else
                {
                    return "La Visita no fue encontrada";
                }
            }
        }


    }
}

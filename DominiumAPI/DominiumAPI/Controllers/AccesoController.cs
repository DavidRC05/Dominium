using DominiumAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DominiumAPI.Controllers
{
    public class AccesoController : ApiController
    {
        [HttpPost]
        [Route("RegistrarCuenta")]
        public string CRegister(userEntity entidad)
        {
            try
            {
                using (var context = new DominiumEntities1())
                {
                    context.RegisterUsers(entidad.FirstName, entidad.LastName, entidad.Email, entidad.PhoneNumber, entidad.Password, entidad.Rol);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public TUsers IniciarSesion(userEntity entidad)
        {
            try
            {
                using (var context = new DominiumEntities1())
                {
                    var propiedad = context.TPropiedades;
                    var user = context.TUsers
                        .FirstOrDefault(x => x.Email == entidad.Email && x.Password == entidad.Password);

                    if (user != null)
                    {
                        var propiedades = user.TPropiedades.ToList(); // Acceso a las propiedades después de cargarlas anticipadamente
                    }

                    return user;
                }
            }
            catch (Exception ex)
            {
                // Registra la excepción en un sistema de registro
                Console.WriteLine($"Error en la autenticación: {ex.Message}");
                return null;
            }
        }


    }
}

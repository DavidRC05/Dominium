using DominiumAPI.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using BCrypt.Net;
using System.Web.Http;

namespace DominiumAPI.Controllers
{
    public class AccesoController : ApiController
    {
        Utilitarios util = new Utilitarios();

        [HttpPost]
        [Route("RegistrarCuenta")]
        public string Register(userEntity entidad)
        {
            try
            {
                using (var context = new DominiumEntities1())
                {
                    // Verifica si el email ya está registrado
                    var existingUser = context.TUsers.FirstOrDefault(u => u.Email == entidad.Email);
                    if (existingUser != null)
                    {
                        // Email ya registrado, retornar un mensaje de error
                        return "EmailYaExiste";
                    }

                    // Hashear la contraseña
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(entidad.Password);

                    // Si el email no está registrado, proceder con el registro
                    context.RegisterUsers(entidad.FirstName, entidad.LastName, entidad.Email, entidad.PhoneNumber, hashedPassword, entidad.Rol);
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


        [HttpPost]
        [Route("RecuperarCuenta")]
        public string RecuperarCuenta(userEntity entidad)
        {
            try
            {
                using (var context = new DominiumEntities1())
                {
                    var datos = (from x in context.TUsers
                                 where x.Email == entidad.Email
                                 select x).FirstOrDefault();

                    if (datos != null)
                    {
                        string urlHtml = AppDomain.CurrentDomain.BaseDirectory + "Templates\\mail.html";
                        string html = File.ReadAllText(urlHtml);

                        html = html.Replace("@@Nombre", datos.FirstName);
                        html = html.Replace("@@Contrasenna", datos.Password);

                        util.EnvioCorreos(datos.Email, "Recuperar Contraseña", html);
                        return "OK";
                    }

                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


    }
}

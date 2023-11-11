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
                using (var context = new DominiumEntities())
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


    }
}

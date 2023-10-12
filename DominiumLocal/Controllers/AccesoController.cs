using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DominiumLocal.Models;
using System.Data.SqlClient;
using System.Data;

namespace DominiumLocal.Controllers
{
    public class AccesoController : Controller
    {
        static string conn = "Data Source=DESKTOP-SV2TH5J;Initial Catalog=DominiumDB;Integrated Security=true";

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CRegister(usuario modelo)
        {
            if (ModelState.IsValid)
            {
                // Aquí debes agregar la lógica para insertar los datos en la base de datos
                // Puedes usar ADO.NET o Entity Framework para esta tarea

                // Ejemplo con Entity Framework
                using (conexiondb contexto = new conexiondb())
                {
                    var usuario = new usuario
                    {
                        FirstName = modelo.FirstName,
                        LastName = modelo.LastName,
                        Email = modelo.Email,
                        PhoneNumber = modelo.PhoneNumber,
                        Password = modelo.Password,
                        Cpassword = modelo.Cpassword
                        // Más campos según tu modelo de datos
                    };

                    contexto.Usuarios.Add(usuario);
                    contexto.SaveChanges();
                }

                // Redirige a una página de confirmación u otra acción después del registro
                return RedirectToAction("Login");
            }

            // Si la validación falla, muestra la vista de registro nuevamente con errores
            return View("Register", modelo);
        }





        public static string ConvertToSHA256(string text)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(text));

                foreach (byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
            }

            return Sb.ToString();
        }

    }
}
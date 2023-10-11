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
        static string conn = "server= DESKTOP-SV2TH5J; database=DominiumDB; integrated security=true";

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        // POST: Acceso/Register (Procesa el formulario de registro)
        [HttpPost]
        public ActionResult CRegister(usuario ousuario)
        {
            if (ousuario.Password == ousuario.Cpassword)
            {
                ousuario.Password = ConvertToSHA256(ousuario.Password);

                try
                {
                    using (SqlConnection cn = new SqlConnection(conn))
                    {
                        cn.Open();

                        string insertSql = "INSERT INTO Users (FirstName, LastName, BirthdayDate, Gender, Email, PhoneNumber, Password) " +
                                           "VALUES (@FirstName, @LastName, @BirthdayDate, @Gender, @Email, @PhoneNumber, @Password)";

                        using (SqlCommand cmd = new SqlCommand(insertSql, cn))
                        {
                            cmd.Parameters.AddWithValue("@FirstName", ousuario.FirstName);
                            cmd.Parameters.AddWithValue("@LastName", ousuario.LastName);
                            cmd.Parameters.AddWithValue("@BirthdayDate", ousuario.BirthdayDate);
                            cmd.Parameters.AddWithValue("@Gender", ousuario.Gender);
                            cmd.Parameters.AddWithValue("@Email", ousuario.Email);
                            cmd.Parameters.AddWithValue("@PhoneNumber", ousuario.PhoneNumber);
                            cmd.Parameters.AddWithValue("@Password", ousuario.Password);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Redirige al usuario a una página de éxito después del registro.
                                return RedirectToAction("Login");
                            }
                            else
                            {
                                ViewBag.Mensaje = "No se pudo insertar el usuario.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = "Se ha producido un error durante el registro: " + ex.Message;
                    
                }
            }
            else
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden.";
            }

            return View("Register"); // Renderiza la vista de registro y muestra mensajes en la vista.
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
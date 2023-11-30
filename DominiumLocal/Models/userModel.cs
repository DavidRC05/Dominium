using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using DominiumLocal.Entity;
namespace DominiumLocal.Models
{
    public class userModel
    {
        public string urlApi = ConfigurationManager.AppSettings["urlApi"];

        public usersEntity Login(usersEntity entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlApi + "IniciarSesion";
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<usersEntity>().Result;
            }
        }

        public string CRegister(usersEntity entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlApi + "RegistrarCuenta";
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public async Task<usersEntity> ObtenerUsuarioPorId(int userId)
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + $"ObtenerUsuarioPorId/{userId}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<usersEntity>();
                }

                // Manejar el error de alguna manera apropiada
                return null;
            }
        }

    }
}
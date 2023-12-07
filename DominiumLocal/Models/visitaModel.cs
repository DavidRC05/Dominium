using DominiumLocal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Configuration;

namespace DominiumLocal.Models
{
    public class visitaModel
    {
        public string urlApi = ConfigurationManager.AppSettings["urlApi"];
        public long AgendarVisita(visitaEntity entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlApi + "AgendarVisita";
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<long>().Result;
            }
        }

        public List<visitaEntity> VerVisitas(int idVendedor)
        {
            using (var client = new HttpClient())
            {
                
                string url = $"{urlApi}GetVisitas/{idVendedor}";
                var resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    var eventos = resp.Content.ReadFromJsonAsync<List<visitaEntity>>().Result;
                    return eventos;
                }

                // Manejar errores si es necesario
                return null;
            }
        }

    }
}
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

        public List<visitaEntity> GetVisitas(int idVendedor)
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

        public List<visitaEntity> VerVisitas()
        {
            using (var client = new HttpClient())
            {

                string url = urlApi + "VerVisitas";
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
        public visitaEntity ConsultaVisita(int q)
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + "ConsultaVisita?q=" + q;
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<visitaEntity>().Result;
            }
        }

        public string ActualizarVisita(visitaEntity entidad)
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + "ActualizarVisita";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(url, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public string EliminarVisita(visitaEntity entidad)
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + "EliminarVisita";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(url, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

    }
}
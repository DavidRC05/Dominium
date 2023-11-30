using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Configuration;
using DominiumLocal.Entity;

namespace DominiumLocal.Models
{
    public class propiedadModel
    {
        public string urlApi = ConfigurationManager.AppSettings["urlApi"];

        public long AgregarPropiedad(propiedadEntity entidad, int idVendedor)
        {
            entidad.IDVendedor = idVendedor;

            using (var client = new HttpClient())
            {
                string url = urlApi + "AgregarPropiedad";
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PostAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<long>().Result;
            }
        }


        public string ActualizarRutaImagen(propiedadEntity entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlApi + "ActualizarRutaImagen";
                JsonContent contenido = JsonContent.Create(entidad);
                var resp = client.PutAsync(url, contenido).Result;
                return resp.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public List<propiedadEntity> ConsultarPropiedades(int idVendedor)
        {
            using (var client = new HttpClient())
            {
                var url = $"{urlApi}ConsultarPropiedades/{idVendedor}";
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<propiedadEntity>>().Result;
            }
        }

        public List<propiedadEntity> VerPropiedades()
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + "VerPropiedades";
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<List<propiedadEntity>>().Result;
            }
        }

        public propiedadEntity ObtenerPropiedadPorId(int propiedadId)
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + $"ObtenerPropiedadPorId/{propiedadId}";
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadFromJsonAsync<propiedadEntity>().Result;
                }

                // Manejar el error de alguna manera apropiada
                return null;
            }
        }


        public propiedadEntity ConsultaPropiedad(long q)
        {
            using (var client = new HttpClient())
            {
                var url = urlApi + "ConsultaPropiedad?q=" + q;
                var res = client.GetAsync(url).Result;
                return res.Content.ReadFromJsonAsync<propiedadEntity>().Result;
            }
        }
    }
}
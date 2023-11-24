using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominiumAPI.Entity
{
    public class propiedadEntity
    {
        public long PropiedadID { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int ProvinciaID { get; set; }
        public string UbicacionExacta { get; set; }
        public int Habitaciones { get; set; }
        public int Banos { get; set; }
        public string Area { get; set; }
        public int Piso { get; set; }
        public string Estacionamiento { get; set; }
        public string Imagen { get; set; }
        public int IDVendedor { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominiumLocal.Entity
{
    public class visitaEntity
    {
        public int VisitaId { get; set; }
        public int VendedorId { get; set; }
        public string NombreVisitante { get; set; }
        public string PropiedadVisitada { get; set; }
        public DateTime FechaVisita { get; set; }
        public int EstadoVisitaId { get; set; }
        public string EstadoVisita { get; set; }
    }
}
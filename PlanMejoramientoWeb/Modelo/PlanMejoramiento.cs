using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Modelo
{
    public class PlanMejoramiento
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaAsignacion { get; set; }

        public DateTime FechaLimite { get; set; }

        public Instructor Instructor { get; set; }

        public TipoPlan TipoPlan { get; set; }
    }
}
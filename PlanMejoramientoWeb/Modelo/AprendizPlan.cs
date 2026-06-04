using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Modelo
{
    public class AprendizPlan
    {
        public int Id { get; set; }

        public Aprendiz Aprendiz { get; set; }

        public PlanMejoramiento PlanMejoramiento { get; set; }

        public string Estado { get; set; }

        public string Observacion { get; set; }

        public DateTime FechaAsignacion { get; set; }
    }
}
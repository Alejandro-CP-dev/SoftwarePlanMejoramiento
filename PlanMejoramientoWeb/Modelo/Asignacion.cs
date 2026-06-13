using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class Asignacion
    {
        public int Id { get; set; }
        public PlanMejoramiento PlanMejoramiento { get; set; }
        public Instructor Instructor { get; set; }
        public Gestor Gestor { get; set; }
        public string Indicacion { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }
}
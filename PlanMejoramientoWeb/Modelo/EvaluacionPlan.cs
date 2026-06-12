using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Modelo
{
    public class EvaluacionPlan
    {
        public int Id { get; set; }
        public string EvaluacionProducto { get; set; }     
        public string EvaluacionConocimiento { get; set; }  
        public string EvaluacionDesempeno { get; set; }     
        public string ResultadoFinal { get; set; }          
        public DateTime FechaEvaluacion { get; set; }
        public PlanMejoramiento PlanMejoramiento { get; set; }
    }
}
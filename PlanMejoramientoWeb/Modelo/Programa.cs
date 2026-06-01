using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Modelo
{
    public class Programa
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Version { get; set; }
        public int Duracion { get; set; }
        public string Estado { get; set; }
        public NivelFormacion NivelFormacion { get; set; }
    }
}
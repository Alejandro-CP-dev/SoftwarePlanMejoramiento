using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Modelo
{
    public class Ficha
    {
        public int Id { get; set; }
        public string CodigoFicha { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public Programa Programa { get; set; }
        public Jornada Jornada { get; set; }
    }
}
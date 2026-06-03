using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Modelo
{
    public class FichaAprendiz
    {
        public int Id { get; set; }

        public Ficha Ficha { get; set; }

        public Aprendiz Aprendiz{ get; set; }
    }
}
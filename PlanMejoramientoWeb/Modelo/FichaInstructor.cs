using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Modelo
{
    public class FichaInstructor
    {
        public int Id { get; set; }

        public Ficha Ficha { get; set; }

        public Instructor Instructor { get; set; }
    }
}
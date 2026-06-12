using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Modelo
{
    public class Gestor
    {
        public int Id { get; set; }

        public string TipoDocumento { get; set; }

        public string NumeroDocumento { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string Contrasena { get; set; }
        public CentroFormacion Centro { get; set; }
        public PlanMejoramiento PlanMejoramiento { get; set; }
    }
}
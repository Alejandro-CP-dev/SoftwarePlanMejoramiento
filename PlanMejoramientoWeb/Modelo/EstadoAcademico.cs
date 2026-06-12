using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Modelo
{
    public class EstadoAcademico
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public string CssBadge
        {
            get
            {
                if (string.IsNullOrEmpty(Nombre)) return "badge-otro";

                string nombre = Nombre.Trim().ToLowerInvariant();

                if (nombre == "cancelado") return "badge-cancelado";
                if (nombre == "activo" || nombre == "en formación" || nombre == "en formacion") return "badge-activo";

                return "badge-otro";
            }
        }
    }
}
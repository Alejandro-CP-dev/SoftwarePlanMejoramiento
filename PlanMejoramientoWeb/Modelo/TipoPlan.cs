using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Modelo
{
    public class TipoPlan
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CssTag
        {
            get { return Id == 2 ? "tag-tipo tag-comite" : "tag-tipo tag-interno"; }
        }

        // Versión HTML completa del badge, usada en pantallas que muestran
        // el tipo como un elemento independiente (ej. Editar Plan).
        public string BadgeHtml
        {
            get
            {
                return Id == 2
                    ? "<span class='badge-comite'>Por Comité</span>"
                    : "<span class='badge-interno'>Interno</span>";
            }
        }
    }
}
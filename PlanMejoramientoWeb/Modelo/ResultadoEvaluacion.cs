using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Modelo
{
    public enum TipoResultadoEvaluacion
    {
        Aprobado,
        NoAprobadoInterno,   // se generó plan comité automáticamente
        NoAprobadoComite,    // aprendiz cancelado automáticamente
        Error
    }

    public class ResultadoEvaluacion
    {
        public TipoResultadoEvaluacion Tipo { get; set; }
        public string Mensaje { get; set; }
        public int IdPlanComiteGenerado { get; set; } // 0 si no aplica

        public static ResultadoEvaluacion MtCrear(TipoResultadoEvaluacion tipo, string mensaje, int idPlanComite = 0)
        {
            return new ResultadoEvaluacion()
            {
                Tipo = tipo,
                Mensaje = mensaje,
                IdPlanComiteGenerado = idPlanComite
            };
        }
    }
}
using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class GestorL
    {
        GestorD oDatos = new GestorD();

        public List<AprendizPlan> MtListarPlanMejoramientoPorAprendiz(int idAprendiz)
        {
            return oDatos.MtListarPlanesPorAprendiz(idAprendiz);
        }

        public bool MtAsignarSupervisorAPlan(PlanMejoramiento asignacion)
        {
           
            return oDatos.MtAsignarSupervisorAPlan(asignacion);
        }
    }
}
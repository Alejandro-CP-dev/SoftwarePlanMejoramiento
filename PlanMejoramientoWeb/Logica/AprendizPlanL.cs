using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class AprendizPlanL
    {
        private AprendizPlanD oAprendizPlan = new AprendizPlanD();

        public bool MtRegistrarAprendizPlan(AprendizPlan asignacion)
        {
            return oAprendizPlan.MtRegistrarAprendizPlan(asignacion);
        }

        public PlanMejoramiento MtObtenerPlanActivo(int idAprendiz)
        {
            return oAprendizPlan.MtObtenerPlanActivo(idAprendiz);
        }

        public List<AprendizPlan> MtListarPlanesPorInstructor(int idInstructor)
        {
            return oAprendizPlan.MtListarPlanesPorInstructor(idInstructor);
        }
    }
}
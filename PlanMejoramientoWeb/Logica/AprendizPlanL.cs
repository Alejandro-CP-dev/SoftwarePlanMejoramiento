using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System.Collections.Generic;

namespace PlanMejoramientoWeb.Logica
{
    public class AprendizPlanL
    {
        private AprendizPlanD oAprendizPlan = new AprendizPlanD();

        public bool MtRegistrarAprendizPlan(AprendizPlan asignacion)
        {
            return oAprendizPlan.MtRegistrarAprendizPlan(asignacion);
        }

        public List<AprendizPlan> MtListarPlanesPorInstructor(int idInstructor)
        {
            return oAprendizPlan.MtListarPlanesPorInstructor(idInstructor);
        }

        public List<AprendizPlan> MtListarPlanesPorAprendiz(int idAprendiz)
        {
            return oAprendizPlan.MtListarPlanesPorAprendiz(idAprendiz);
        }
    }
}
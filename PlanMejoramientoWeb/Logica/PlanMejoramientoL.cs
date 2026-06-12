using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System.Collections.Generic;

namespace PlanMejoramientoWeb.Logica
{
    public class PlanMejoramientoL
    {
        private readonly PlanMejoramientoD oPlanD = new PlanMejoramientoD();

        public int MtRegistrarPlanMejoramiento(PlanMejoramiento plan)
        {
            return oPlanD.MtRegistrarPlanMejoramiento(plan);
        }

        public List<PlanMejoramiento> MtListarPlanMejoramiento()
        {
            return oPlanD.MtListarPlanMejoramiento();
        }

        public PlanMejoramiento MtObtenerPlanMejoramientoId(int id)
        {
            return oPlanD.MtObtenerPlanMejoramientoId(id);
        }

        public List<PlanMejoramiento> MtListarPlanesPorInstructor(int idInstructor)
        {
            return oPlanD.MtListarPlanesPorInstructor(idInstructor);
        }

        public bool MtActualizarPlanMejoramiento(PlanMejoramiento plan)
        {
            return oPlanD.MtActualizarPlanMejoramiento(plan);
        }

        public bool MtActualizarObservacionPlan(int idPlan, string observacion)
        {
            return oPlanD.MtActualizarObservacionPlan(idPlan, observacion);
        }
    }
}
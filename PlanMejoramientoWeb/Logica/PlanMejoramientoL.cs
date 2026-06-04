using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class PlanMejoramientoL
    {
        private PlanMejoramientoD oPlanMejoramiento = new PlanMejoramientoD();

        public int MtRegistrarPlanMejoramiento(PlanMejoramiento plan)
        {
            return oPlanMejoramiento.MtRegistrarPlanMejoramiento(plan);
        }

        public List<PlanMejoramiento> MtListarPlanMejoramiento()
        {
            return oPlanMejoramiento.MtListarPlanMejoramiento();
        }

        public PlanMejoramiento MtObtenerPlanMejoramientoId(int id)
        {
            return oPlanMejoramiento.MtObtenerPlanMejoramientoId(id);
        }
    }
}
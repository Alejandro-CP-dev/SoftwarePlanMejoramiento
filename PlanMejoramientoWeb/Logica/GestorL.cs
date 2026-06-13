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

        public List<AprendizPlan> MtListarPlanMejoramientoPorCentro(int idCentro)
        {
            return oDatos.MtListarPlanesPorCentro(idCentro);
        }

        public string MtAsignarSupervisorAPlan(Asignacion asignacion, int idInstructorCreador)
        {
            if (asignacion.Instructor.Id == idInstructorCreador)
            {
            }

            if (asignacion.Instructor.Id <= 0)
            {
                return "Debe seleccionar un instructor supervisor válido.";
            }

            if (asignacion.Gestor == null || asignacion.Gestor.Id <= 0)
            {
                return "No se identificó el gestor que realiza la asignación.";
            }

            bool resultado = oDatos.MtAsignarSupervisorAPlan(asignacion);

            return resultado ? null : "No se pudo registrar la asignación.";
        }

        public Asignacion MtObtenerAsignacionVigente(int idPlanMejoramiento)
        {
            return oDatos.MtObtenerAsignacionVigente(idPlanMejoramiento);
        }
    }
}
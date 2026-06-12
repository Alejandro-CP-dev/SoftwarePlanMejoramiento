using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;

namespace PlanMejoramientoWeb.Logica
{
    public class EvaluacionPlanL
    {
        // IdTipoPlan: 1 = Interno, 2 = Comité
        private const int TIPO_PLAN_INTERNO = 1;
        private const int TIPO_PLAN_COMITE = 2;

        // IdEstadoAcademico: 6 = Cancelado
        private const int ESTADO_ACADEMICO_CANCELADO = 6;

        private readonly EvaluacionPlanD oEvaluacionD = new EvaluacionPlanD();

        public ResultadoEvaluacion MtProcesarEvaluacion(EvaluacionPlan evaluacion, int idInstructor)
        {
            bool aprueba =
                evaluacion.EvaluacionProducto == "Aprueba" &&
                evaluacion.EvaluacionConocimiento == "Aprueba" &&
                evaluacion.EvaluacionDesempeno == "Aprueba";

            evaluacion.ResultadoFinal = aprueba ? "Aprobado" : "No Aprobado";
            evaluacion.FechaEvaluacion = DateTime.Now;

            bool registrado = oEvaluacionD.MtRegistrarEvaluacion(evaluacion);
            if (!registrado)
                return ResultadoEvaluacion.MtCrear(TipoResultadoEvaluacion.Error,
                    "Error al registrar la evaluación. Intente nuevamente.");

            int idPlan = evaluacion.PlanMejoramiento.Id;
            int tipoPlan = oEvaluacionD.MtObtenerTipoPlan(idPlan);
            int idAprendiz = oEvaluacionD.MtObtenerIdAprendizPorPlan(idPlan);

            if (aprueba)
            {
                oEvaluacionD.MtActualizarEstadoAprendizPlan(idPlan, "Aprobado");
                return ResultadoEvaluacion.MtCrear(TipoResultadoEvaluacion.Aprobado,
                    "El aprendiz aprobó el plan. El resultado de aprendizaje ha sido superado.");
            }

            // No aprueba
            oEvaluacionD.MtActualizarEstadoAprendizPlan(idPlan, "No Aprobado");

            if (tipoPlan == TIPO_PLAN_INTERNO)
            {
                int idPlanComite = oEvaluacionD.MtCrearPlanComite(idPlan, idInstructor, idAprendiz);

                if (idPlanComite > 0)
                {
                    return ResultadoEvaluacion.MtCrear(TipoResultadoEvaluacion.NoAprobadoInterno,
                        "El aprendiz no aprobó el plan interno. Se generó automáticamente un Plan por Comité (Id: " + idPlanComite + "). Puede editarlo desde la lista de planes.",
                        idPlanComite);
                }

                return ResultadoEvaluacion.MtCrear(TipoResultadoEvaluacion.Error,
                    "El aprendiz no aprobó. Ocurrió un error al generar el plan por comité.");
            }

            if (tipoPlan == TIPO_PLAN_COMITE)
            {
                if (idAprendiz > 0)
                    oEvaluacionD.MtCancelarAprendiz(idAprendiz, ESTADO_ACADEMICO_CANCELADO);

                return ResultadoEvaluacion.MtCrear(TipoResultadoEvaluacion.NoAprobadoComite,
                    "El aprendiz no aprobó el plan por comité. Su estado académico ha sido cambiado a CANCELADO automáticamente.");
            }

            return ResultadoEvaluacion.MtCrear(TipoResultadoEvaluacion.Error, "El aprendiz no aprobó el plan.");
        }

        // ── Reglas de presentación que la Vista necesita pero no debe decidir ──

        public bool MtEsPlanComite(int idTipoPlan)
        {
            return idTipoPlan == TIPO_PLAN_COMITE;
        }

        public string MtNombreTipoResultado(TipoResultadoEvaluacion tipo)
        {
            switch (tipo)
            {
                case TipoResultadoEvaluacion.Aprobado: return "exito";
                case TipoResultadoEvaluacion.NoAprobadoInterno: return "alerta";
                case TipoResultadoEvaluacion.NoAprobadoComite: return "error";
                default: return "error";
            }
        }
    }
}
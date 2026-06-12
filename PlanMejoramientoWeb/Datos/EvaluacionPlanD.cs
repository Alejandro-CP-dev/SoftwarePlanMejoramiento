using PlanMejoramientoWeb.Modelo;
using System;
using System.Data.SqlClient;

namespace PlanMejoramientoWeb.Datos
{
    public class EvaluacionPlanD
    {
        public bool MtRegistrarEvaluacion(EvaluacionPlan evaluacion)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = @"
                    INSERT INTO EvaluacionPlan
                    (EvaluacionProducto, EvaluacionConocimiento, EvaluacionDesempeno, ResultadoFinal, FechaEvaluacion, IdPlanMejoramiento)
                    VALUES
                    (@Producto, @Conocimiento, @Desempeno, @ResultadoFinal, @FechaEvaluacion, @IdPlan)";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Producto", evaluacion.EvaluacionProducto);
                cmd.Parameters.AddWithValue("@Conocimiento", evaluacion.EvaluacionConocimiento);
                cmd.Parameters.AddWithValue("@Desempeno", evaluacion.EvaluacionDesempeno);
                cmd.Parameters.AddWithValue("@ResultadoFinal", evaluacion.ResultadoFinal);
                cmd.Parameters.AddWithValue("@FechaEvaluacion", evaluacion.FechaEvaluacion);
                cmd.Parameters.AddWithValue("@IdPlan", evaluacion.PlanMejoramiento.Id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool MtActualizarEstadoAprendizPlan(int idPlanMejoramiento, string nuevoEstado)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = @"UPDATE AprendizPlan SET Estado = @Estado WHERE IdPlanMejoramiento = @IdPlan";
                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                cmd.Parameters.AddWithValue("@IdPlan", idPlanMejoramiento);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool MtCancelarAprendiz(int idAprendiz, int idEstadoAcademico)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = @"UPDATE Aprendiz SET IdEstadoAcademico = @IdEstado WHERE Id = @IdAprendiz";
                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@IdEstado", idEstadoAcademico);
                cmd.Parameters.AddWithValue("@IdAprendiz", idAprendiz);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int MtObtenerIdAprendizPorPlan(int idPlanMejoramiento)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = @"SELECT IdAprendiz FROM AprendizPlan WHERE IdPlanMejoramiento = @IdPlan";
                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@IdPlan", idPlanMejoramiento);
                object resultado = cmd.ExecuteScalar();
                return resultado != null ? Convert.ToInt32(resultado) : 0;
            }
        }

        public int MtObtenerTipoPlan(int idPlan)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = "SELECT IdTipoPlan FROM PlanMejoramiento WHERE Id = @IdPlan";
                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@IdPlan", idPlan);
                object resultado = cmd.ExecuteScalar();
                return resultado != null ? Convert.ToInt32(resultado) : 0;
            }
        }

        // ── Crear plan por comité vacío (para que el instructor lo edite) ──
        public int MtCrearPlanComite(int idPlanInternoOriginal, int idInstructor, int idAprendiz)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                // Obtener nombre del plan interno para el nombre provisional
                string consultaOrigen = "SELECT Nombre FROM PlanMejoramiento WHERE Id = @IdPlan";
                SqlCommand cmdOrigen = new SqlCommand(consultaOrigen, conn);
                cmdOrigen.Parameters.AddWithValue("@IdPlan", idPlanInternoOriginal);
                string nombreOriginal = cmdOrigen.ExecuteScalar()?.ToString() ?? "Plan";

                // Crear el plan comité con datos provisionales
                string consultaInsert = @"
                    INSERT INTO PlanMejoramiento (Nombre, FechaAsignacion, FechaLimite, IdInstructor, IdTipoPlan)
                    OUTPUT INSERTED.Id
                    VALUES (@Nombre, @FechaAsignacion, @FechaLimite, @IdInstructor, 2)";

                SqlCommand cmdInsert = new SqlCommand(consultaInsert, conn);
                cmdInsert.Parameters.AddWithValue("@Nombre", "Comité - " + nombreOriginal);
                cmdInsert.Parameters.AddWithValue("@FechaAsignacion", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@FechaLimite", DateTime.Now.AddDays(15));
                cmdInsert.Parameters.AddWithValue("@IdInstructor", idInstructor);

                object idNuevo = cmdInsert.ExecuteScalar();
                if (idNuevo == null) return 0;

                int idPlanComite = Convert.ToInt32(idNuevo);

                // Asignar al mismo aprendiz en estado Activo
                string consultaAsignar = @"
                    INSERT INTO AprendizPlan (IdAprendiz, IdPlanMejoramiento, Estado, Observacion, FechaAsignacion)
                    VALUES (@IdAprendiz, @IdPlan, 'Activo', 'Plan generado automáticamente por comité. Pendiente de edición por el instructor.', @Fecha)";

                SqlCommand cmdAsignar = new SqlCommand(consultaAsignar, conn);
                cmdAsignar.Parameters.AddWithValue("@IdAprendiz", idAprendiz);
                cmdAsignar.Parameters.AddWithValue("@IdPlan", idPlanComite);
                cmdAsignar.Parameters.AddWithValue("@Fecha", DateTime.Now);
                cmdAsignar.ExecuteNonQuery();

                return idPlanComite;
            }
        }
    }
}
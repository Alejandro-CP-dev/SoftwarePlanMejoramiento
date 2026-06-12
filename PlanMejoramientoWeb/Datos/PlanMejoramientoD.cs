using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PlanMejoramientoWeb.Datos
{
    public class PlanMejoramientoD
    {
        public int MtRegistrarPlanMejoramiento(PlanMejoramiento plan)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = @"
                    INSERT INTO PlanMejoramiento (Nombre, FechaAsignacion, FechaLimite, IdInstructor, IdTipoPlan)
                    OUTPUT INSERTED.Id
                    VALUES (@Nombre, @FechaAsignacion, @FechaLimite, @IdInstructor, @IdTipoPlan)";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Nombre", plan.Nombre);
                cmd.Parameters.AddWithValue("@FechaAsignacion", plan.FechaAsignacion);
                cmd.Parameters.AddWithValue("@FechaLimite", plan.FechaLimite);
                cmd.Parameters.AddWithValue("@IdInstructor", plan.Instructor.Id);
                cmd.Parameters.AddWithValue("@IdTipoPlan", plan.TipoPlan.Id);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<PlanMejoramiento> MtListarPlanMejoramiento()
        {
            List<PlanMejoramiento> lista = new List<PlanMejoramiento>();
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = @"
                    SELECT p.*, i.Nombre AS NombreInstructor, tp.Nombre AS NombreTipoPlan
                    FROM PlanMejoramiento p
                    INNER JOIN Instructor i  ON p.IdInstructor = i.Id
                    INNER JOIN TipoPlan tp   ON p.IdTipoPlan   = tp.Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new PlanMejoramiento()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString(),
                        FechaAsignacion = Convert.ToDateTime(dr["FechaAsignacion"]),
                        FechaLimite = Convert.ToDateTime(dr["FechaLimite"]),
                        Instructor = new Instructor() { Nombre = dr["NombreInstructor"].ToString() },
                        TipoPlan = new TipoPlan() { Nombre = dr["NombreTipoPlan"].ToString() }
                    });
                }
            }
            return lista;
        }

        public PlanMejoramiento MtObtenerPlanMejoramientoId(int id)
        {
            PlanMejoramiento plan = null;
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = @"
                    SELECT p.*, tp.Nombre AS NombreTipoPlan
                    FROM PlanMejoramiento p
                    INNER JOIN TipoPlan tp ON p.IdTipoPlan = tp.Id
                    WHERE p.Id = @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    plan = new PlanMejoramiento()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString(),
                        FechaAsignacion = Convert.ToDateTime(dr["FechaAsignacion"]),
                        FechaLimite = Convert.ToDateTime(dr["FechaLimite"]),
                        Instructor = new Instructor() { Id = Convert.ToInt32(dr["IdInstructor"]) },
                        TipoPlan = new TipoPlan()
                        {
                            Id = Convert.ToInt32(dr["IdTipoPlan"]),
                            Nombre = dr["NombreTipoPlan"].ToString()
                        }
                    };
                }
            }
            return plan;
        }

        // ── Lista los planes activos asignados a aprendices de un instructor ──
        public List<PlanMejoramiento> MtListarPlanesPorInstructor(int idInstructor)
        {
            List<PlanMejoramiento> lista = new List<PlanMejoramiento>();
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = @"
                    SELECT DISTINCT
                        p.Id, p.Nombre, p.FechaAsignacion, p.FechaLimite,
                        tp.Id AS IdTipo, tp.Nombre AS NombreTipoPlan
                    FROM PlanMejoramiento p
                    INNER JOIN TipoPlan tp    ON p.IdTipoPlan = tp.Id
                    INNER JOIN AprendizPlan ap ON ap.IdPlanMejoramiento = p.Id
                    WHERE p.IdInstructor = @IdInstructor
                    ORDER BY p.FechaAsignacion DESC";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@IdInstructor", idInstructor);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new PlanMejoramiento()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString(),
                        FechaAsignacion = Convert.ToDateTime(dr["FechaAsignacion"]),
                        FechaLimite = Convert.ToDateTime(dr["FechaLimite"]),
                        TipoPlan = new TipoPlan()
                        {
                            Id = Convert.ToInt32(dr["IdTipo"]),
                            Nombre = dr["NombreTipoPlan"].ToString()
                        }
                    });
                }
            }
            return lista;
        }

        // ── Actualizar nombre, fecha límite y observación de un plan ─────
        public bool MtActualizarPlanMejoramiento(PlanMejoramiento plan)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = @"
                    UPDATE PlanMejoramiento
                    SET Nombre      = @Nombre,
                        FechaLimite = @FechaLimite
                    WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Nombre", plan.Nombre);
                cmd.Parameters.AddWithValue("@FechaLimite", plan.FechaLimite);
                cmd.Parameters.AddWithValue("@Id", plan.Id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ── Actualizar observación en AprendizPlan ────────────────────────
        public bool MtActualizarObservacionPlan(int idPlan, string observacion)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = @"
                    UPDATE AprendizPlan
                    SET Observacion = @Observacion
                    WHERE IdPlanMejoramiento = @IdPlan";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Observacion", observacion);
                cmd.Parameters.AddWithValue("@IdPlan", idPlan);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
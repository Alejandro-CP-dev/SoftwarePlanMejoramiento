using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class AprendizPlanD
    {
        public bool MtRegistrarAprendizPlan(AprendizPlan asignacion)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"INSERT INTO AprendizPlan
                                    (
                                        IdAprendiz,
                                        IdPlanMejoramiento,
                                        Estado,
                                        Observacion,
                                        FechaAsignacion
                                    )
                                    VALUES
                                    (
                                        @IdAprendiz,
                                        @IdPlanMejoramiento,
                                        @Estado,
                                        @Observacion,
                                        @FechaAsignacion
                                    )";
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@IdAprendiz", asignacion.Aprendiz.Id);
                cmd.Parameters.AddWithValue("@IdPlanMejoramiento", asignacion.PlanMejoramiento.Id);
                cmd.Parameters.AddWithValue("@Estado", asignacion.Estado);
                cmd.Parameters.AddWithValue("@Observacion", asignacion.Observacion);
                cmd.Parameters.AddWithValue("@FechaAsignacion", asignacion.FechaAsignacion);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public PlanMejoramiento MtObtenerPlanActivo(int idAprendiz)
        {
            PlanMejoramiento plan = null;

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                                    SELECT TOP 1

                                        pm.Id,
                                        pm.Nombre,
                                        pm.FechaAsignacion,
                                        pm.FechaLimite,

                                        tp.Id AS IdTipoPlan,
                                        tp.Nombre AS TipoPlan

                                    FROM AprendizPlan ap

                                    INNER JOIN PlanMejoramiento pm
                                        ON ap.IdPlanMejoramiento = pm.Id

                                    INNER JOIN TipoPlan tp
                                        ON pm.IdTipoPlan = tp.Id

                                    WHERE ap.IdAprendiz = @IdAprendiz
                                    AND ap.Estado = 'Activo'

                                    ORDER BY pm.FechaAsignacion DESC";
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@IdAprendiz", idAprendiz);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    plan = new PlanMejoramiento()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString(),
                        FechaAsignacion = Convert.ToDateTime(dr["FechaAsignacion"]),
                        FechaLimite = Convert.ToDateTime(dr["FechaLimite"]),

                        TipoPlan = new TipoPlan()
                        {
                            Id = Convert.ToInt32(dr["IdTipoPlan"]),
                            Nombre = dr["TipoPlan"].ToString()
                        }
                    };
                }

            }
            return plan;
        }

        public List<AprendizPlan> MtListarPlanesPorInstructor(int idInstructor)
        {
            List<AprendizPlan> lista = new List<AprendizPlan>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT
                        ap.Id,
                        ap.Estado,
                        ap.FechaAsignacion,
                        a.Id         AS IdAprendiz,
                        a.Nombre     AS NombreAprendiz,
                        a.Apellido   AS ApellidoAprendiz,
                        pm.Id        AS IdPlan,
                        pm.Nombre    AS NombrePlan,
                        pm.FechaLimite,
                        tp.Nombre    AS TipoPlan
                    FROM AprendizPlan ap
                    INNER JOIN Aprendiz a
                        ON ap.IdAprendiz = a.Id
                    INNER JOIN PlanMejoramiento pm
                        ON ap.IdPlanMejoramiento = pm.Id
                    INNER JOIN TipoPlan tp
                        ON pm.IdTipoPlan = tp.Id
                    WHERE pm.IdInstructor = @IdInstructor
                      AND ap.Estado = 'Activo'
                    ORDER BY ap.FechaAsignacion DESC";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@IdInstructor", idInstructor);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new AprendizPlan()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Estado = dr["Estado"].ToString(),
                        FechaAsignacion = Convert.ToDateTime(dr["FechaAsignacion"]),

                        Aprendiz = new Aprendiz()
                        {
                            Id = Convert.ToInt32(dr["IdAprendiz"]),
                            Nombre = dr["NombreAprendiz"].ToString(),
                            Apellido = dr["ApellidoAprendiz"].ToString()
                        },

                        PlanMejoramiento = new PlanMejoramiento()
                        {
                            Id = Convert.ToInt32(dr["IdPlan"]),
                            Nombre = dr["NombrePlan"].ToString(),
                            FechaLimite = Convert.ToDateTime(dr["FechaLimite"]),
                            TipoPlan = new TipoPlan() { Nombre = dr["TipoPlan"].ToString() }
                        }
                    });
                }
            }

            return lista;
        }
    }
}
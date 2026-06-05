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
    }
}
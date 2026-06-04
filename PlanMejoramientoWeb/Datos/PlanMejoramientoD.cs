using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
                                    INSERT INTO PlanMejoramiento
                                    (
                                        Nombre,
                                        FechaAsignacion,
                                        FechaLimite,
                                        IdInstructor,
                                        IdTipoPlan
                                    )
                                    OUTPUT INSERTED.Id
                                    VALUES
                                    (
                                        @Nombre,
                                        @FechaAsignacion,
                                        @FechaLimite,
                                        @IdInstructor,
                                        @IdTipoPlan
                                    )";

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

                string consulta = @"SELECT
                                    p.*,

                                    i.Nombre AS NombreInstructor,

                                    tp.Nombre AS NombreTipoPlan

                                FROM PlanMejoramiento p

                                INNER JOIN Instructor i
                                    ON p.IdInstructor = i.Id

                                INNER JOIN TipoPlan tp
                                    ON p.IdTipoPlan = tp.Id";

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

                        Instructor = new Instructor()
                        {
                            Nombre =
                                dr["NombreInstructor"]
                                .ToString()
                        },

                        TipoPlan = new TipoPlan()
                        {
                            Nombre =
                                dr["NombreTipoPlan"]
                                .ToString()
                        }
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

                string consulta = @"SELECT * FROM PlanMejoramiento WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    plan = new PlanMejoramiento()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString(),
                        FechaAsignacion = Convert.ToDateTime(dr["FechaAsignacion"]),
                        FechaLimite = Convert.ToDateTime(dr["FechaLimite"]),

                        Instructor = new Instructor()
                        {
                            Id =
                            Convert.ToInt32(
                                dr["IdInstructor"])
                        },

                        TipoPlan = new TipoPlan()
                        {
                            Id =
                            Convert.ToInt32(
                                dr["IdTipoPlan"])
                        }
                    };

                }
            }
            return plan; 
        }
    }
}
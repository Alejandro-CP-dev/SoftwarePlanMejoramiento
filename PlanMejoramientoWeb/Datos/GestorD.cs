using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class GestorD
    {
        public List<AprendizPlan> MtListarPlanesPorAprendiz(int idAprendiz)
        {
            List<AprendizPlan> lista = new List<AprendizPlan>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT
                        ap.Id,
                        a.Nombre,
                        a.Apellido,
                        a.NumeroDocumento,
                        ea.Nombre AS EstadoAcademico,
                        pm.FechaLimite,
                        pm.FechaAsignacion,
                        tp.Nombre    AS TipoPlan
                    FROM AprendizPlan ap
                    INNER JOIN Aprendiz a
                        ON ap.IdAprendiz = a.Id
                    INNER JOIN EstadoAcademico ea
                        ON a.IdEstadoAcademico = ea.Id
                    INNER JOIN PlanMejoramiento pm
                        ON ap.IdPlanMejoramiento = pm.Id
                    INNER JOIN TipoPlan tp
                        ON pm.IdTipoPlan = tp.Id
                    WHERE ap.Id = @IdAprendiz";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@IdAprendiz", idAprendiz);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new AprendizPlan()
                    {
                        Id = Convert.ToInt32(dr["Id"]),

                        Aprendiz = new Aprendiz() 
                        {
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            NumeroDocumento = dr["NumeroDocumento"].ToString(),
                            EstadoAcademico = new EstadoAcademico()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["EstadoAcademico"].ToString()
                            }
                        },

                        PlanMejoramiento = new PlanMejoramiento()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            FechaAsignacion = Convert.ToDateTime(dr["FechaAsignacion"]),
                            FechaLimite = Convert.ToDateTime(dr["FechaLimite"]),
                            TipoPlan = new TipoPlan()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["TipoPlan"].ToString()
                            }
                        }
                    });
                }
            }

            return lista;
        }

        public bool MtAsignarSupervisorAPlan(PlanMejoramiento asignacion)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"INSERT INTO PlanMejoramiento
                                    (
                                        IdPlanMejoramiento,
                                        IdInstructor,
                                    )
                                    VALUES
                                    (                                        
                                        @IdPlanMejoramiento,
                                        @IdInstructor
                                    )";
                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@IdPlanMejoramiento", asignacion.Id);
                cmd.Parameters.AddWithValue("@IdInstructor", asignacion.Instructor.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }
}
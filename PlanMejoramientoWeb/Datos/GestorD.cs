using PlanMejoramientoWeb.Logica;
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
        public List<AprendizPlan> MtListarPlanesPorCentro(int idCentro)
        {
            List<AprendizPlan> lista = new List<AprendizPlan>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT
                        ap.Id,
                        a.NumeroDocumento,
                        a.Nombre,
                        a.Apellido,
                        ea.Nombre AS EstadoAcademico,
                        f.CodigoFicha,
                        prog.Nombre AS Programa,
                        j.Nombre AS Jornada,
                        pm.Id AS IdPlan,
                        pm.Nombre AS NombrePlan,
                        pm.FechaAsignacion,
                        pm.FechaLimite,
                        tp.Nombre AS TipoPlan,
                        pm.IdInstructor
                    FROM AprendizPlan ap
                    INNER JOIN Aprendiz a ON ap.IdAprendiz = a.Id
                    INNER JOIN EstadoAcademico ea ON a.IdEstadoAcademico = ea.Id
                    INNER JOIN PlanMejoramiento pm ON ap.IdPlanMejoramiento = pm.Id
                    INNER JOIN TipoPlan tp ON pm.IdTipoPlan = tp.Id
                    INNER JOIN FichaAprendiz fa ON fa.IdAprendiz = a.Id
                    INNER JOIN Ficha f ON f.Id = fa.IdFicha
                    INNER JOIN Programa prog ON prog.Id = f.IdPrograma
                    INNER JOIN Jornada j ON j.Id = f.IdJornada
                    INNER JOIN CentroPrograma cp ON cp.IdPrograma = prog.Id
                    WHERE cp.IdCentro = @IdCentro";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@IdCentro", idCentro);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new AprendizPlan()
                    {
                        Id = Convert.ToInt32(dr["Id"]),

                        Aprendiz = new Aprendiz()
                        {
                            NumeroDocumento = dr["NumeroDocumento"] == DBNull.Value ? "" : dr["NumeroDocumento"].ToString(),
                            Nombre = dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"] == DBNull.Value ? "" : dr["Apellido"].ToString(),
                            EstadoAcademico = new EstadoAcademico()
                            {
                                Nombre = dr["EstadoAcademico"] == DBNull.Value ? "" : dr["EstadoAcademico"].ToString()
                            }
                        },

                        PlanMejoramiento = new PlanMejoramiento()
                        {
                            Id = Convert.ToInt32(dr["IdPlan"]),
                            Nombre = dr["NombrePlan"] == DBNull.Value ? "" : dr["NombrePlan"].ToString(),
                            FechaAsignacion = dr["FechaAsignacion"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["FechaAsignacion"]),
                            FechaLimite = dr["FechaLimite"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["FechaLimite"]),
                            TipoPlan = new TipoPlan()
                            {
                                Nombre = dr["TipoPlan"] == DBNull.Value ? "" : dr["TipoPlan"].ToString()
                            },
                            Instructor = new Instructor()
                            {
                                Id = dr["IdInstructor"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdInstructor"])
                            }
                        }
                    });
                }
            }

            return lista;
        }

        public bool MtAsignarSupervisorAPlan(Asignacion asignacion)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"INSERT INTO Asignacion
                                    (IdPlanMejoramiento, IdInstructor, IdGestor, Indicacion, FechaAsignacion)
                                    VALUES
                                    (@IdPlanMejoramiento, @IdInstructor, @IdGestor, @Indicacion, @FechaAsignacion)";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@IdPlanMejoramiento", asignacion.PlanMejoramiento.Id);
                cmd.Parameters.AddWithValue("@IdInstructor", asignacion.Instructor.Id);
                cmd.Parameters.AddWithValue("@IdGestor", asignacion.Gestor.Id);
                cmd.Parameters.AddWithValue("@Indicacion",
                    string.IsNullOrEmpty(asignacion.Indicacion) ? (object)DBNull.Value : asignacion.Indicacion);
                cmd.Parameters.AddWithValue("@FechaAsignacion", DateTime.Now);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public Asignacion MtObtenerAsignacionVigente(int idPlanMejoramiento)
        {
            Asignacion asignacion = null;

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"SELECT TOP 1 * FROM Asignacion
                                    WHERE IdPlanMejoramiento = @IdPlanMejoramiento
                                    ORDER BY FechaAsignacion DESC";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@IdPlanMejoramiento", idPlanMejoramiento);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    asignacion = new Asignacion()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        PlanMejoramiento = new PlanMejoramiento() { Id = Convert.ToInt32(dr["IdPlanMejoramiento"]) },
                        Instructor = new Instructor() { Id = Convert.ToInt32(dr["IdInstructor"]) },
                        Gestor = new Gestor() { Id = Convert.ToInt32(dr["IdGestor"]) },
                        Indicacion = dr["Indicacion"] == DBNull.Value ? "" : dr["Indicacion"].ToString(),
                        FechaAsignacion = Convert.ToDateTime(dr["FechaAsignacion"])
                    };
                }
            }

            return asignacion;
        }
    }
}
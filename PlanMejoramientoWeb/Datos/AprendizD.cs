using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class AprendizD
    {
        public bool MtRegistrarAprendiz(Aprendiz aprendiz)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"INSERT INTO Aprendiz
                                    (
                                        TipoDocumento,
                                        NumeroDocumento,
                                        Nombre,
                                        Apellido,
                                        Correo,
                                        Telefono,
                                        Contrasena,
                                        IdEstadoAcademico
                                    )
                                    VALUES
                                    (
                                        @TipoDocumento,
                                        @NumeroDocumento,
                                        @Nombre,
                                        @Apellido,
                                        @Correo,
                                        @Telefono,
                                        @Contrasena,
                                        @IdEstadoAcademico
                                    )";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@TipoDocumento", aprendiz.TipoDocumento);
                cmd.Parameters.AddWithValue("@NumeroDocumento", aprendiz.NumeroDocumento);
                cmd.Parameters.AddWithValue("@Nombre", aprendiz.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", aprendiz.Apellido);
                cmd.Parameters.AddWithValue("@Correo", aprendiz.Correo);
                cmd.Parameters.AddWithValue("@Telefono", aprendiz.Telefono);
                cmd.Parameters.AddWithValue("@Contrasena", aprendiz.Contrasena);
                cmd.Parameters.AddWithValue("@IdEstadoAcademico", aprendiz.EstadoAcademico.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int MtRegistrarAprendizRetornandoId(Aprendiz aprendiz)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
            INSERT INTO Aprendiz
            (
                TipoDocumento,
                NumeroDocumento,
                Nombre,
                Apellido,
                Correo,
                Telefono,
                Contrasena,
                IdEstadoAcademico
            )
            OUTPUT INSERTED.Id
            VALUES
            (
                @TipoDocumento,
                @NumeroDocumento,
                @Nombre,
                @Apellido,
                @Correo,
                @Telefono,
                @Contrasena,
                @IdEstadoAcademico
            )";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@TipoDocumento", aprendiz.TipoDocumento);
                cmd.Parameters.AddWithValue("@NumeroDocumento", aprendiz.NumeroDocumento);
                cmd.Parameters.AddWithValue("@Nombre", aprendiz.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", aprendiz.Apellido);
                cmd.Parameters.AddWithValue("@Correo", aprendiz.Correo);
                cmd.Parameters.AddWithValue("@Telefono", aprendiz.Telefono);
                cmd.Parameters.AddWithValue("@Contrasena", aprendiz.Contrasena);
                cmd.Parameters.AddWithValue("@IdEstadoAcademico", aprendiz.EstadoAcademico.Id);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool MtExisteDocumento(string numeroDocumento)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"SELECT COUNT(*) 
                            FROM Aprendiz
                            WHERE NumeroDocumento=@NumeroDocumento";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@NumeroDocumento", numeroDocumento);

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public List<Aprendiz> MtListarAprendiz()
        {
            List<Aprendiz> lista = new List<Aprendiz>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT
                        a.*,
                        ea.Nombre AS EstadoAcademico
                    FROM Aprendiz a
                    INNER JOIN EstadoAcademico ea
                        ON a.IdEstadoAcademico = ea.Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Aprendiz()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        TipoDocumento = dr["TipoDocumento"].ToString(),
                        NumeroDocumento = dr["NumeroDocumento"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        Apellido = dr["Apellido"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Contrasena = dr["Contrasena"].ToString(),

                        EstadoAcademico = new EstadoAcademico()
                        {
                            Nombre = dr["EstadoAcademico"].ToString()
                        }
                    });
                }
            }

            return lista;
        }

        public Aprendiz MtObtenerAprendizPorId(int id)
        {
            Aprendiz aprendiz = null;

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT
                        a.*,
                        ea.Id AS EstadoAcademicoId,
                        ea.Nombre AS EstadoAcademico
                    FROM Aprendiz a
                    INNER JOIN EstadoAcademico ea
                        ON a.IdEstadoAcademico = ea.Id
                    WHERE a.Id = @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    aprendiz = new Aprendiz()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        TipoDocumento = dr["TipoDocumento"].ToString(),
                        NumeroDocumento = dr["NumeroDocumento"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        Apellido = dr["Apellido"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Contrasena = dr["Contrasena"].ToString(),

                        EstadoAcademico = new EstadoAcademico()
                        {
                            Id = Convert.ToInt32(dr["EstadoAcademicoId"]),
                            Nombre = dr["EstadoAcademico"].ToString()
                        }
                    };
                }
            }

            return aprendiz;
        }

        public bool MtActualizarAprendiz(Aprendiz aprendiz)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    UPDATE Aprendiz SET
                        TipoDocumento = @TipoDocumento,
                        NumeroDocumento = @NumeroDocumento,
                        Nombre = @Nombre,
                        Apellido = @Apellido,
                        Correo = @Correo,
                        Telefono = @Telefono,
                        Contrasena = @Contrasena,
                        IdEstadoAcademico = @IdEstadoAcademico
                    WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", aprendiz.Id);
                cmd.Parameters.AddWithValue("@TipoDocumento", aprendiz.TipoDocumento);
                cmd.Parameters.AddWithValue("@NumeroDocumento", aprendiz.NumeroDocumento);
                cmd.Parameters.AddWithValue("@Nombre", aprendiz.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", aprendiz.Apellido);
                cmd.Parameters.AddWithValue("@Correo", aprendiz.Correo);
                cmd.Parameters.AddWithValue("@Telefono", aprendiz.Telefono);
                cmd.Parameters.AddWithValue("@Contrasena", aprendiz.Contrasena);
                cmd.Parameters.AddWithValue("@IdEstadoAcademico", aprendiz.EstadoAcademico.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool MtEliminarAprendiz(int id)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = "BEGIN TRANSACTION DELETE FROM FichaAprendiz WHERE IdAprendiz=@Id DELETE FROM Aprendiz WHERE Id=@Id COMMIT";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Aprendiz> MtListarAprendizPorInstructor(int idInstructor)
        {
            List<Aprendiz> lista = new List<Aprendiz>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"SELECT DISTINCT
                                        a.*,
                                        ea.Id AS IdEstadoAcademico,
                                        ea.Nombre AS EstadoAcademico
                                    FROM Instructor i

                                    INNER JOIN FichaInstructor fi
                                        ON i.Id = fi.IdInstructor

                                    INNER JOIN FichaAprendiz fa
                                        ON fi.IdFicha = fa.IdFicha

                                    INNER JOIN Aprendiz a
                                        ON fa.IdAprendiz = a.Id

                                    INNER JOIN EstadoAcademico ea
                                        ON a.IdEstadoAcademico = ea.Id

                                    WHERE i.Id = @IdInstructor";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@IdInstructor", idInstructor);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Aprendiz()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        NumeroDocumento = dr["NumeroDocumento"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        Apellido = dr["Apellido"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Telefono = dr["Telefono"].ToString(),

                        EstadoAcademico = new EstadoAcademico()
                        {
                            Id = Convert.ToInt32(dr["IdEstadoAcademico"]),
                            Nombre = dr["EstadoAcademico"].ToString()
                        }
                    });
                }
            }
            return lista;
        }

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

                cmd.Parameters.AddWithValue("@IdAprendiz", asignacion.Aprendiz);
                cmd.Parameters.AddWithValue("@IdPlanMejoramiento", asignacion.PlanMejoramiento.Id);
                cmd.Parameters.AddWithValue("@Estado", asignacion.Estado);
                cmd.Parameters.AddWithValue("@Observacion", asignacion.Observacion);
                cmd.Parameters.AddWithValue("@FechaAsignacion", asignacion.FechaAsignacion);

                return cmd.ExecuteNonQuery() > 0;

            }
        }
    }
}
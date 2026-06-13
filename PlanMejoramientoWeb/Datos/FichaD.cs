using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class FichaD
    {
        public bool MtRegistrarFicha(Ficha ficha)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    INSERT INTO Ficha
                    (
                        CodigoFicha,
                        FechaInicio,
                        FechaFinalizacion,
                        Descripcion,
                        Estado,
                        IdPrograma,
                        IdJornada
                    )
                    VALUES
                    (
                        @CodigoFicha,
                        @FechaInicio,
                        @FechaFinalizacion,
                        @Descripcion,
                        @Estado,
                        @IdPrograma,
                        @IdJornada
                    )";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@CodigoFicha", ficha.CodigoFicha);
                cmd.Parameters.AddWithValue("@FechaInicio", ficha.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFinalizacion", ficha.FechaFinalizacion);
                cmd.Parameters.AddWithValue("@Descripcion",
                    string.IsNullOrEmpty(ficha.Descripcion) ? (object)DBNull.Value : ficha.Descripcion);
                cmd.Parameters.AddWithValue("@Estado", ficha.Estado);
                cmd.Parameters.AddWithValue("@IdPrograma", ficha.Programa.Id);
                cmd.Parameters.AddWithValue("@IdJornada", ficha.Jornada.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Ficha> MtListarFicha()
        {
            List<Ficha> lista = new List<Ficha>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT
                        f.Id,
                        f.CodigoFicha,
                        f.FechaInicio,
                        f.FechaFinalizacion,
                        f.Descripcion,
                        f.Estado,
                        p.Id AS IdPrograma,
                        p.Nombre AS Programa,
                        j.Id AS IdJornada,
                        j.Nombre AS Jornada
                    FROM Ficha f
                    INNER JOIN Programa p
                        ON f.IdPrograma = p.Id
                    INNER JOIN Jornada j
                        ON f.IdJornada = j.Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Ficha()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        CodigoFicha = dr["CodigoFicha"] == DBNull.Value ? "" : dr["CodigoFicha"].ToString(),
                        FechaInicio = dr["FechaInicio"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["FechaInicio"]),
                        FechaFinalizacion = dr["FechaFinalizacion"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["FechaFinalizacion"]),
                        Descripcion = dr["Descripcion"] == DBNull.Value ? "" : dr["Descripcion"].ToString(),
                        Estado = dr["Estado"] == DBNull.Value ? "" : dr["Estado"].ToString(),

                        Programa = new Programa()
                        {
                            Id = Convert.ToInt32(dr["IdPrograma"]),
                            Nombre = dr["Programa"] == DBNull.Value ? "" : dr["Programa"].ToString()
                        },

                        Jornada = new Jornada()
                        {
                            Id = Convert.ToInt32(dr["IdJornada"]),
                            Nombre = dr["Jornada"] == DBNull.Value ? "" : dr["Jornada"].ToString()
                        }
                    });
                }
            }

            return lista;
        }

        public Ficha MtObtenerFichaPorId(int id)
        {
            Ficha ficha = null;

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT *
                    FROM Ficha
                    WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    ficha = MtMapearFichaBasica(dr);
                }
            }

            return ficha;
        }

        public Ficha MtObtenerFichaPorCodigo(string codigo)
        {
            Ficha ficha = null;

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT *
                    FROM Ficha
                    WHERE CodigoFicha = @CodigoFicha";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@CodigoFicha", codigo);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    ficha = MtMapearFichaBasica(dr);
                }
            }

            return ficha;
        }

        // Mapea una fila de la tabla Ficha (sin JOINs) a un objeto Ficha,
        // dejando solo el Id de Programa/Jornada (sin nombre).
        private Ficha MtMapearFichaBasica(SqlDataReader dr)
        {
            return new Ficha()
            {
                Id = Convert.ToInt32(dr["Id"]),
                CodigoFicha = dr["CodigoFicha"] == DBNull.Value ? "" : dr["CodigoFicha"].ToString(),
                FechaInicio = dr["FechaInicio"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["FechaInicio"]),
                FechaFinalizacion = dr["FechaFinalizacion"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["FechaFinalizacion"]),
                Descripcion = dr["Descripcion"] == DBNull.Value ? "" : dr["Descripcion"].ToString(),
                Estado = dr["Estado"] == DBNull.Value ? "" : dr["Estado"].ToString(),

                Programa = new Programa()
                {
                    Id = dr["IdPrograma"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdPrograma"])
                },

                Jornada = new Jornada()
                {
                    Id = dr["IdJornada"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdJornada"])
                }
            };
        }

        public bool MtActualizarFicha(Ficha ficha)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    UPDATE Ficha
                    SET
                        CodigoFicha = @CodigoFicha,
                        FechaInicio = @FechaInicio,
                        FechaFinalizacion = @FechaFinalizacion,
                        Descripcion = @Descripcion,
                        Estado = @Estado,
                        IdPrograma = @IdPrograma,
                        IdJornada = @IdJornada
                    WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", ficha.Id);
                cmd.Parameters.AddWithValue("@CodigoFicha", ficha.CodigoFicha);
                cmd.Parameters.AddWithValue("@FechaInicio", ficha.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFinalizacion", ficha.FechaFinalizacion);
                cmd.Parameters.AddWithValue("@Descripcion",
                    string.IsNullOrEmpty(ficha.Descripcion) ? (object)DBNull.Value : ficha.Descripcion);
                cmd.Parameters.AddWithValue("@Estado", ficha.Estado);
                cmd.Parameters.AddWithValue("@IdPrograma", ficha.Programa.Id);
                cmd.Parameters.AddWithValue("@IdJornada", ficha.Jornada.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Verifica si el código de ficha ya existe (para otro registro distinto a idExcluir)
        public bool MtExisteCodigoFicha(string codigoFicha, int idExcluir)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = "SELECT COUNT(*) FROM Ficha WHERE CodigoFicha = @CodigoFicha AND Id <> @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@CodigoFicha", codigoFicha);
                cmd.Parameters.AddWithValue("@Id", idExcluir);

                int total = (int)cmd.ExecuteScalar();
                return total > 0;
            }
        }

        // Verifica si la ficha tiene aprendices o instructores asignados
        public bool MtTieneAsignaciones(int idFicha)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT
                        (SELECT COUNT(*) FROM FichaAprendiz WHERE IdFicha = @IdFicha) +
                        (SELECT COUNT(*) FROM FichaInstructor WHERE IdFicha = @IdFicha)";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@IdFicha", idFicha);

                int total = (int)cmd.ExecuteScalar();
                return total > 0;
            }
        }

        public bool MtEliminarFicha(int id)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = "DELETE FROM Ficha WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
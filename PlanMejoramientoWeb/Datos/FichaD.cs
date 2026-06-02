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
                cmd.Parameters.AddWithValue("@Descripcion", ficha.Descripcion);
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
                        f.*,
                        p.Nombre AS Programa,
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
                        CodigoFicha = dr["CodigoFicha"].ToString(),
                        FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                        FechaFinalizacion = Convert.ToDateTime(dr["FechaFinalizacion"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Estado = dr["Estado"].ToString(),

                        Programa = new Programa()
                        {
                            Nombre = dr["Programa"].ToString()
                        },

                        Jornada = new Jornada()
                        {
                            Nombre = dr["Jornada"].ToString()
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
                    WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    ficha = new Ficha()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        CodigoFicha = dr["CodigoFicha"].ToString(),
                        FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                        FechaFinalizacion = Convert.ToDateTime(dr["FechaFinalizacion"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Estado = dr["Estado"].ToString(),

                        Programa = new Programa()
                        {
                            Id = Convert.ToInt32(dr["IdPrograma"])
                        },

                        Jornada = new Jornada()
                        {
                            Id = Convert.ToInt32(dr["IdJornada"])
                        }
                    };
                }
            }

            return ficha;
        }

        public bool MtActualizarFicha(Ficha ficha)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    UPDATE Ficha
                    SET
                        CodigoFicha=@CodigoFicha,
                        FechaInicio=@FechaInicio,
                        FechaFinalizacion=@FechaFinalizacion,
                        Descripcion=@Descripcion,
                        Estado=@Estado,
                        IdPrograma=@IdPrograma,
                        IdJornada=@IdJornada
                    WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", ficha.Id);
                cmd.Parameters.AddWithValue("@CodigoFicha", ficha.CodigoFicha);
                cmd.Parameters.AddWithValue("@FechaInicio", ficha.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFinalizacion", ficha.FechaFinalizacion);
                cmd.Parameters.AddWithValue("@Descripcion", ficha.Descripcion);
                cmd.Parameters.AddWithValue("@Estado", ficha.Estado);
                cmd.Parameters.AddWithValue("@IdPrograma", ficha.Programa.Id);
                cmd.Parameters.AddWithValue("@IdJornada", ficha.Jornada.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool MtEliminarFicha(int id)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = "DELETE FROM Ficha WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class FichaInstructorD
    {
        public bool MtRegistrarFichaInstructor(FichaInstructor fichaInstructor)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"INSERT INTO FichaInstructor
                                    (IdFicha, IdInstructor)
                                    VALUES
                                    (@IdFicha, @IdInstructor)";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@IdFicha",
                    fichaInstructor.Ficha.Id);

                cmd.Parameters.AddWithValue("@IdInstructor",
                    fichaInstructor.Instructor.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Ficha> MtListarFichaInstructor(int idInstructor)
        {
            List<Ficha> lista = new List<Ficha>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT
                        f.*,
                        j.Id AS IdJornada,
                        j.Nombre AS NombreJornada
                    FROM FichaInstructor fi
                    INNER JOIN Ficha f
                        ON fi.IdFicha = f.Id
                    INNER JOIN Jornada j
                        ON f.IdJornada = j.Id
                    WHERE fi.IdInstructor = @IdInstructor";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@IdInstructor", idInstructor);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Ficha()
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        CodigoFicha = dr["CodigoFicha"].ToString(),
                        FechaInicio = DateTime.Parse(dr["FechaInicio"].ToString()),
                        FechaFinalizacion = DateTime.Parse(dr["FechaFinalizacion"].ToString()),

                        Jornada = new Jornada()
                        {
                            Id = Convert.ToInt32(dr["IdJornada"]),
                            Nombre = dr["NombreJornada"].ToString()
                        },

                        Estado = dr["Estado"].ToString()

                    });
                }
            }

            return lista;
        }

        public bool MtEliminarFichaInstructor(int id)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta =
                    "DELETE FROM FichaInstructor WHERE Id=@Id";

                SqlCommand cmd =
                    new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Ficha> MtListarFichasPorInstructor(int idInstructor)
        {
            List<Ficha> lista = new List<Ficha>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
            SELECT f.*
            FROM FichaInstructor fi
            INNER JOIN Ficha f
                ON fi.IdFicha = f.Id
            WHERE fi.IdInstructor = @IdInstructor";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue(
                    "@IdInstructor",
                    idInstructor);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Ficha()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        CodigoFicha = dr["CodigoFicha"].ToString(),
                        Estado = dr["Estado"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}
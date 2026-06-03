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

        public List<FichaInstructor> MtListarFichaInstructor()
        {
            List<FichaInstructor> lista = new List<FichaInstructor>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT
                        fi.Id,

                        f.Id AS IdFicha,
                        f.CodigoFicha,

                        i.Id AS IdInstructor,
                        i.Nombre,
                        i.Apellido

                    FROM FichaInstructor fi

                    INNER JOIN Ficha f
                        ON fi.IdFicha = f.Id

                    INNER JOIN Instructor i
                        ON fi.IdInstructor = i.Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new FichaInstructor()
                    {
                        Id = Convert.ToInt32(dr["Id"]),

                        Ficha = new Ficha()
                        {
                            Id = Convert.ToInt32(dr["IdFicha"]),
                            CodigoFicha = dr["CodigoFicha"].ToString()
                        },

                        Instructor = new Instructor()
                        {
                            Id = Convert.ToInt32(dr["IdInstructor"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString()
                        }
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
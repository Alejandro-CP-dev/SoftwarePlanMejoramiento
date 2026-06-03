using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class FichaAprendizD
    {
        public bool MtRegistrarFichaAprendiz(FichaAprendiz fichaAprendiz)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"INSERT INTO FichaAprendiz
                                    (IdFicha, IdAprendiz)
                                    VALUES
                                    (@IdFicha, @IdAprendiz)";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@IdFicha",
                    fichaAprendiz.Ficha.Id);

                cmd.Parameters.AddWithValue("@IdAprendiz",
                    fichaAprendiz.Aprendiz.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<FichaAprendiz> MtListarFichaAprendiz()
        {
            List<FichaAprendiz> lista = new List<FichaAprendiz>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT
                        fa.Id,

                        f.Id AS IdFicha,
                        f.CodigoFicha,

                        a.Id AS IdAprendiz,
                        a.Nombre,
                        a.Apellido

                    FROM FichaAprendiz fa

                    INNER JOIN Ficha f
                        ON fa.IdFicha = f.Id

                    INNER JOIN Aprendiz a
                        ON fa.IdAprendiz = a.Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new FichaAprendiz()
                    {
                        Id = Convert.ToInt32(dr["Id"]),

                        Ficha = new Ficha()
                        {
                            Id = Convert.ToInt32(dr["IdFicha"]),
                            CodigoFicha = dr["CodigoFicha"].ToString()
                        },

                        Aprendiz = new Aprendiz()
                        {
                            Id = Convert.ToInt32(dr["IdAprendiz"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString()
                        }
                    });
                }
            }

            return lista;
        }

        public bool MtEliminarFichaAprendiz(int id)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta =
                    "DELETE FROM FichaAprendiz WHERE Id=@Id";

                SqlCommand cmd =
                    new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
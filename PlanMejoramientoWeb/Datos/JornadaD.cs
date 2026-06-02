using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class JornadaD
    {
        public bool MtRegistrarJornada(Jornada jornada)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    INSERT INTO Jornada(Nombre)
                    VALUES(@Nombre)";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Nombre", jornada.Nombre);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Jornada> MtListarJornada()
        {
            List<Jornada> lista = new List<Jornada>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = "SELECT * FROM Jornada";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Jornada()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString()
                    });
                }
            }

            return lista;
        }

        public Jornada MtObtenerJornadaPorId(int id)
        {
            Jornada jornada = null;

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta =
                    "SELECT * FROM Jornada WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    jornada = new Jornada()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString()
                    };
                }
            }

            return jornada;
        }

        public bool MtActualizarJornada(Jornada jornada)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    UPDATE Jornada
                    SET Nombre=@Nombre
                    WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", jornada.Id);
                cmd.Parameters.AddWithValue("@Nombre", jornada.Nombre);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool MtEliminarJornada(int id)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta =
                    "DELETE FROM Jornada WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
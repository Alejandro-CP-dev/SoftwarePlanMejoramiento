using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class ProgramaD
    {
        public bool MtRegistrarPrograma(Programa programa)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"INSERT INTO Programa
                                      (Codigo, Nombre, Version, Duracion, Estado, IdNivel)
                                      VALUES
                                      (@Codigo, @Nombre, @Version, @Duracion, @Estado, @IdNivel)";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Codigo", programa.Codigo);
                cmd.Parameters.AddWithValue("@Nombre", programa.Nombre);
                cmd.Parameters.AddWithValue("@Version", programa.Version);
                cmd.Parameters.AddWithValue("@Duracion", programa.Duracion);
                cmd.Parameters.AddWithValue("@Estado", programa.Estado);
                cmd.Parameters.AddWithValue("@IdNivel", programa.NivelFormacion.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Programa> MtListarPrograma()
        {
            List<Programa> lista = new List<Programa>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                                    SELECT
                                        p.Id,
                                        p.Codigo,
                                        p.Nombre,
                                        p.Version,
                                        p.Duracion,
                                        p.Estado,

                                        nf.Nombre AS NivelFormacion

                                    FROM Programa p
                                    INNER JOIN NivelFormacion nf
                                        ON p.IdNivel = nf.Id";
                SqlCommand cmd = new SqlCommand(consulta, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Programa()
                    {
                        Id = (int)dr["Id"],
                        Codigo = dr["Codigo"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        Version = dr["Version"].ToString(),
                        Duracion = (int)dr["Duracion"],
                        Estado = dr["Estado"].ToString(),
                        NivelFormacion = new NivelFormacion
                        {
                            Nombre = dr["NivelFormacion"].ToString()
                        }
                    });
                }
            }
            return lista;
        }

        public Programa MtObtenerProgramaPorId(int id)
        {
            Programa programa = null;

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();
                string consulta = @"
                                    SELECT
                                        p.*,
                                        nf.Id AS NivelId,
                                        nf.Nombre AS NivelNombre
                                    FROM Programa p
                                    INNER JOIN NivelFormacion nf
                                        ON p.IdNivel = nf.Id
                                    WHERE p.Id = @Id";
                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    programa = new Programa()
                    {
                        Id = (int)dr["Id"],
                        Codigo = dr["Codigo"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        Version = dr["Version"].ToString(),
                        Duracion = (int)dr["Duracion"],
                        Estado = dr["Estado"].ToString(),
                        NivelFormacion = new NivelFormacion
                        {
                            Nombre = dr["NivelFormacion"].ToString()
                        }
                    };
                }
            }
            return programa;
        }

        public bool MtActualizarPrograma(Programa programa)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"UPDATE Programa SET
                                        Codigo=@Codigo,
                                        Nombre=@Nombre,
                                        Version=@Version,
                                        Duracion=@Duracion,
                                        Estado=@Estado,
                                        IdNivel=@IdNivel
                                        WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", programa.Id);
                cmd.Parameters.AddWithValue("@Codigo", programa.Codigo);
                cmd.Parameters.AddWithValue("@Nombre", programa.Nombre);
                cmd.Parameters.AddWithValue("@Version", programa.Version);
                cmd.Parameters.AddWithValue("@Duracion", programa.Duracion);
                cmd.Parameters.AddWithValue("@Estado", programa.Estado);
                cmd.Parameters.AddWithValue("@IdNivel", programa.NivelFormacion.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool MtEliminarPrograma(int id)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = "DELETE FROM Programa WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
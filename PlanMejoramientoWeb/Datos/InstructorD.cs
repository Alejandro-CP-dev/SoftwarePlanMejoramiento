using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class InstructorD
    {
        public bool MtRegistrarInstructor(Instructor instructor)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"INSERT INTO Instructor
                                    (
                                        TipoDocumento,
                                        NumeroDocumento,
                                        Nombre,
                                        Apellido,
                                        Correo,
                                        Telefono,
                                        Contrasena,
                                        Estado
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
                                        @Estado
                                    )";
                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@TipoDocumento", instructor.TipoDocumento);
                cmd.Parameters.AddWithValue("@NumeroDocumento", instructor.NumeroDocumento);
                cmd.Parameters.AddWithValue("@Nombre", instructor.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", instructor.Apellido);
                cmd.Parameters.AddWithValue("@Correo", instructor.Correo);
                cmd.Parameters.AddWithValue("@Telefono", instructor.Telefono);
                cmd.Parameters.AddWithValue("@Contrasena", instructor.Contrasena);
                cmd.Parameters.AddWithValue("@Estado", instructor.Estado);

                return cmd.ExecuteNonQuery() > 0; 
            }
        }

        public List<Instructor> MtListarInstructor()
        {
            List<Instructor> lista = new List<Instructor>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = "SELECT * FROM Instructor";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Instructor()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        TipoDocumento = dr["TipoDocumento"].ToString(),
                        NumeroDocumento = dr["NumeroDocumento"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        Apellido = dr["Apellido"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Contrasena = dr["Contrasena"].ToString(),
                        Estado = dr["Estado"].ToString()
                    });
                }
            }

            return lista;
        }

        public Instructor MtObtenerInstructorPorId(int id)
        {
            Instructor instructor = null;

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = "SELECT * FROM Instructor WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    instructor = new Instructor()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        TipoDocumento = dr["TipoDocumento"].ToString(),
                        NumeroDocumento = dr["NumeroDocumento"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        Apellido = dr["Apellido"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Contrasena = dr["Contrasena"].ToString(),
                        Estado = dr["Estado"].ToString()
                    };
                }
            }
            return instructor;
        }

        public bool MtActualizarInstructor(Instructor instructor)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"UPDATE Instructor SET
                                    TipoDocumento = @TipoDocumento,
                                    NumeroDocumento = @NumeroDocumento,
                                    Nombre = @Nombre,
                                    Apellido = @Apellido,
                                    Correo = @Correo,
                                    Telefono = @Telefono,
                                    Contrasena = @Contrasena,
                                    Estado = @Estado
                                    WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", instructor.Id);
                cmd.Parameters.AddWithValue("@TipoDocumento", instructor.TipoDocumento);
                cmd.Parameters.AddWithValue("@NumeroDocumento", instructor.NumeroDocumento);
                cmd.Parameters.AddWithValue("@Nombre", instructor.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", instructor.Apellido);
                cmd.Parameters.AddWithValue("@Correo", instructor.Correo);
                cmd.Parameters.AddWithValue("@Telefono", instructor.Telefono);
                cmd.Parameters.AddWithValue("@Contrasena", instructor.Contrasena);
                cmd.Parameters.AddWithValue("@Estado", instructor.Estado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool MtEliminarInstructor(int id)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = "DELETE FROM Instructor WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
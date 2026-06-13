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
                    lista.Add(MtMapearInstructor(dr));
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
                    instructor = MtMapearInstructor(dr);
                }
            }
            return instructor;
        }

        // Verifica si ya existe un instructor con ese correo (para otro registro distinto a idExcluir)
        public bool MtExisteCorreo(string correo, int idExcluir)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = "SELECT COUNT(*) FROM Instructor WHERE Correo = @Correo AND Id <> @Id";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Id", idExcluir);

                int total = (int)cmd.ExecuteScalar();
                return total > 0;
            }
        }

        // Mapea una fila de la tabla Instructor a un objeto Instructor, protegiendo contra DBNull.
        private Instructor MtMapearInstructor(SqlDataReader dr)
        {
            return new Instructor()
            {
                Id = Convert.ToInt32(dr["Id"]),
                TipoDocumento = dr["TipoDocumento"] == DBNull.Value ? "" : dr["TipoDocumento"].ToString(),
                NumeroDocumento = dr["NumeroDocumento"] == DBNull.Value ? "" : dr["NumeroDocumento"].ToString(),
                Nombre = dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"].ToString(),
                Apellido = dr["Apellido"] == DBNull.Value ? "" : dr["Apellido"].ToString(),
                Correo = dr["Correo"] == DBNull.Value ? "" : dr["Correo"].ToString(),
                Telefono = dr["Telefono"] == DBNull.Value ? "" : dr["Telefono"].ToString(),
                Contrasena = dr["Contrasena"] == DBNull.Value ? "" : dr["Contrasena"].ToString(),
                Estado = dr["Estado"] == DBNull.Value ? "" : dr["Estado"].ToString()
            };
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

        // Verifica si el instructor tiene Planes de Mejoramiento o Asignaciones de supervisión asociadas
        public bool MtTienePlanesOAsignaciones(int idInstructor)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    SELECT
                        (SELECT COUNT(*) FROM PlanMejoramiento WHERE IdInstructor = @Id) +
                        (SELECT COUNT(*) FROM Asignacion WHERE IdInstructor = @Id)";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Id", idInstructor);

                int total = (int)cmd.ExecuteScalar();
                return total > 0;
            }
        }

        public bool MtEliminarInstructor(int id)
        {
            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = @"
                    BEGIN TRANSACTION;
                    DELETE FROM FichaInstructor WHERE IdInstructor = @Id;
                    DELETE FROM InstructorEspecialidad WHERE IdInstructor = @Id;
                    DELETE FROM Instructor WHERE Id = @Id;
                    COMMIT;";

                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
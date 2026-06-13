using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class LoginD
    {
        public UsuarioSesion MtIniciarSesion(string correo, string clave)
        {
            UsuarioSesion usuario = null;

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                // Sesion Como Administrador
                string consultaAdmin = @"
                                        SELECT *
                                        FROM Administrador
                                        WHERE Correo=@Correo
                                        AND Contrasena=@Contrasena";
                SqlCommand cmdAdmin = new SqlCommand(consultaAdmin, conn);

                cmdAdmin.Parameters.AddWithValue("@Correo", correo);
                cmdAdmin.Parameters.AddWithValue("@Contrasena", clave);

                SqlDataReader drAdmin = cmdAdmin.ExecuteReader();

                if (drAdmin.Read())
                {
                    usuario = new UsuarioSesion()
                    {
                        Id = Convert.ToInt32(drAdmin["Id"]),
                        Nombre = drAdmin["Nombre"].ToString(),
                        Apellido = drAdmin["Apellido"].ToString(),
                        Correo = drAdmin["Correo"].ToString(),

                        Rol = "Administrador"
                    };
                }
                drAdmin.Close();

                if (usuario != null)
                {
                    return usuario;
                }

                // Sesion Como Gestor 

                string consultaGestor = @"
                                        SELECT *
                                        FROM Gestor
                                        WHERE Correo=@Correo
                                        AND Contrasena=@Contrasena";
                SqlCommand cmdGestor = new SqlCommand(consultaGestor, conn);

                cmdGestor.Parameters.AddWithValue("@Correo", correo);
                cmdGestor.Parameters.AddWithValue("@Contrasena", clave);

                SqlDataReader drGestor = cmdGestor.ExecuteReader();

                if (drGestor.Read())
                {
                    usuario = new UsuarioSesion()
                    {
                        Id = Convert.ToInt32(drGestor["Id"]),
                        Nombre = drGestor["Nombre"].ToString(),
                        Apellido = drGestor["Apellido"].ToString(),
                        Correo = drGestor["Correo"].ToString(),
                        IdCentro = drGestor["IdCentro"] == DBNull.Value ? 0 : Convert.ToInt32(drGestor["IdCentro"]),

                        Rol = "Gestor"
                    };
                }
                drGestor.Close();

                if (usuario != null)
                {
                    return usuario;
                }


                // Sesion Como Instructor
                string consultaInstructor = @"
                                        SELECT *
                                        FROM Instructor
                                        WHERE Correo=@Correo
                                        AND Contrasena=@Contrasena";

                SqlCommand cmdInst = new SqlCommand(consultaInstructor, conn);

                cmdInst.Parameters.AddWithValue("@Correo", correo);
                cmdInst.Parameters.AddWithValue("@Contrasena", clave);

                SqlDataReader drInst = cmdInst.ExecuteReader();

                if (drInst.Read())
                {
                    usuario = new UsuarioSesion()
                    {
                        Id = Convert.ToInt32(drInst["Id"]),
                        Nombre = drInst["Nombre"].ToString(),
                        Apellido = drInst["Apellido"].ToString(),
                        Correo = drInst["Correo"].ToString(),

                        Rol = "Instructor"

                    };
                }
                drInst.Close();

                if (usuario != null)
                {
                    return usuario;
                }

                // Sesion Como Aprendiz
                string consultaAprendiz = @"
                                        SELECT *
                                        FROM Aprendiz
                                        WHERE Correo=@Correo
                                        AND Contrasena=@Contrasena";

                SqlCommand cmdApz = new SqlCommand(consultaAprendiz, conn);

                cmdApz.Parameters.AddWithValue("@Correo", correo);
                cmdApz.Parameters.AddWithValue("@Contrasena", clave);

                SqlDataReader drApz = cmdApz.ExecuteReader();

                if (drApz.Read())
                {
                    usuario = new UsuarioSesion()
                    {
                        Id = Convert.ToInt32(drApz["Id"]),
                        Nombre = drApz["Nombre"].ToString(),
                        Apellido = drApz["Apellido"].ToString(),
                        Correo = drApz["Correo"].ToString(),

                        Rol = "Aprendiz"
                    };
                }

                return usuario;
            }
        }
    }
}
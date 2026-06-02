using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class EstadoAcademicoD
    {
        public List<EstadoAcademico> MtListarEstadoAcademico()
        {
            List<EstadoAcademico> lista = new List<EstadoAcademico>();

            using (SqlConnection conn = ConexionDB.MtAbrirConexion())
            {
                conn.Open();

                string consulta = "SELECT * FROM EstadoAcademico";

                SqlCommand cmd = new SqlCommand(consulta, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new EstadoAcademico()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString(),
                        Descripcion = dr["Descripcion"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}
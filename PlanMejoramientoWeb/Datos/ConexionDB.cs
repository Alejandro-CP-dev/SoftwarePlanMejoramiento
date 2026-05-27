using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Datos
{
    public class ConexionDB
    {
        private static readonly string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

        public static SqlConnection MtAbrirConexion()
        {
            if (string.IsNullOrEmpty(cadena))
            {
                throw new Exception("Error en la configuracion de la conxecion");
            }

            return new SqlConnection(cadena);
        }
    }
}
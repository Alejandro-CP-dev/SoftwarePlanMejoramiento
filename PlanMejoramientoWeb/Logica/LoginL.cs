using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class LoginL
    {
        private LoginD oLoginD = new LoginD();

        public UsuarioSesion MtIniciarSesion(string correo, string clave)
        {
            if (string.IsNullOrEmpty(correo))
            {
                return null;
            }
            if (string.IsNullOrEmpty(clave))
            {
                return null;
            }

            return oLoginD.MtIniciarSesion(correo, clave);
        }
    }
}
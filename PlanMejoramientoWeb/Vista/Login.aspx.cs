using PlanMejoramientoWeb.Logica;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramientoWeb.Vista
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            LoginL oLoginL = new LoginL();

            UsuarioSesion usuario = oLoginL.MtIniciarSesion(txtCorreo.Text, txtContrasena.Text);

            if (usuario != null)
            {
                Session["Usuario"] = usuario;

                switch (usuario.Rol)
                {
                    case "Administrador":
                        Response.Redirect("Administrador/Inicio.aspx");
                        break;
                    case "Gestor":
                        Response.Redirect("Gestores/Inicio.aspx");
                        break;

                    case "Instructor":
                        Response.Redirect("Instructor/Inicio.aspx");
                        break;

                    case "Aprendiz":
                        Response.Redirect("Aprendiz/Inicio.aspx");
                        break;
                    default:
                        ClientScript.RegisterStartupScript(
                            this.GetType(),
                            "mensaje",
                            "alert('Rol no reconocido');",
                            true);
                        break;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Correo o contraseña incorrectos');",
                    true);
            }
        }
    }
}
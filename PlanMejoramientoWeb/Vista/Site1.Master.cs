using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramientoWeb.Vista
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            MtCargarMenu();
        }

        public void MtCargarMenu()
        {
            UsuarioSesion usuario = (UsuarioSesion)Session["Usuario"];

            lblUsuario.Text = usuario.Nombre + " " + usuario.Apellido;
            lblRol.Text = usuario.Rol;

            pnlAdministrador.Visible = false;
            pnlInstructor.Visible = false;
            pnlAprendiz.Visible = false;
            pnlGestor.Visible = false;

            switch (usuario.Rol)
            {
                case "Administrador":
                    pnlAdministrador.Visible = true;
                    break;
                case "Gestor":
                    pnlGestor.Visible = true;
                    break;
                case "Instructor":
                    pnlInstructor.Visible = true;
                    break;
                case "Aprendiz":
                    pnlAprendiz.Visible = true;
                    break;
            }

        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Vista/Login.aspx");
        }
    }
}
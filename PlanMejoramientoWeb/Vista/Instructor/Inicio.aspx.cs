using PlanMejoramientoWeb.Logica;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramientoWeb.Vista.Instructor
{
    public partial class Inicio : System.Web.UI.Page
    {
        private FichaInstructorL oFichaInstructor = new FichaInstructorL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarFichas();
            }
        } 

        public void MtCargarFichas()
        {
            UsuarioSesion usuario = Session["Usuario"] as UsuarioSesion;

            gvFichas.DataSource = oFichaInstructor.MtListarFichaInstructor(usuario.Id);
            gvFichas.DataBind();
        }
    }
}
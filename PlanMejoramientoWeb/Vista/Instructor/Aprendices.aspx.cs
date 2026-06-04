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
    public partial class Aprendices : System.Web.UI.Page
    {
        private AprendizL oAprendiz = new AprendizL();
        protected void Page_Load(object sender, EventArgs e)
        {
            MtCargarAprendices();
        }
        public void MtCargarAprendices()
        {
            UsuarioSesion usuario =
                Session["Usuario"] as UsuarioSesion;

            gvAprendices.DataSource =
                oAprendiz.MtListarAprendicesPorInstructor(
                    usuario.Id);

            gvAprendices.DataBind();
        }
    }
}
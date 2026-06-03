using PlanMejoramientoWeb.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramientoWeb.Vista.Administrador
{
    public partial class Inicio : System.Web.UI.Page
    {
        private ProgramaL oPrograma = new ProgramaL();
        private FichaL oFichaL = new FichaL();
        private InstructorL oInstructorL = new InstructorL();
        private AprendizL oAprendizL = new AprendizL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMetricas();
                CargarRecientes();
            }
            
        }
        private void CargarMetricas()
        {
            lblTotalProgramas.Text = oPrograma.MtListarPrograma().Count.ToString();
            lblTotalFichas.Text = oFichaL.MtListarFicha().Count.ToString();
            lblTotalInstructores.Text = oInstructorL.MtListarInstructor().Count.ToString();
            lblTotalAprendices.Text = oAprendizL.MtListarAprendiz().Count.ToString();
        }

        private void CargarRecientes()
        {
            gvFichasRecientes.DataSource = oFichaL.MtListarFicha().OrderByDescending(f => f.Id).Take(5).ToList();
            gvFichasRecientes.DataBind();

            gvInstructoresRecientes.DataSource = oInstructorL.MtListarInstructor()
                .OrderByDescending(i => i.Id).Take(5).ToList();
            gvInstructoresRecientes.DataBind();
        }
    }
}
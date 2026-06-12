using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Logica;
using PlanMejoramientoWeb.Modelo;
using PlanMejoramientoWeb.Vista.Administrador;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramientoWeb.Vista.Gestores
{
    public partial class Inicio : System.Web.UI.Page
    {
        private InstructorL oInstructor = new InstructorL();
        private PlanMejoramientoL oPlan = new PlanMejoramientoL();
        private GestorL oGestor = new GestorL();
        private AprendizPlanL oPlanMejoramiento = new AprendizPlanL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
                MtCargarPlanesAsignar();
            }
        }

        public void MtCargarPlanesAsignar()
        {
            ddlSupervisorAsignar.DataSource = oInstructor.MtListarInstructor();
            ddlSupervisorAsignar.DataTextField = "Nombre";
            ddlSupervisorAsignar.DataValueField = "Id";
            ddlSupervisorAsignar.DataBind();

            ddlSupervisorAsignar.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }

        private void CargarDatos()
        {
            UsuarioSesion usuario = Session["Usuario"] as UsuarioSesion;

            gvPlanes.DataSource = oGestor.MtListarPlanMejoramientoPorAprendiz(usuario.Id);
            gvPlanes.DataBind();
        }

        protected void btnAsignarInstructor_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int idPlanMejoramiento = Convert.ToInt32(btn.CommandArgument);

            PlanMejoramiento pm = oPlan.MtObtenerPlanMejoramientoId(idPlanMejoramiento);

            if (pm != null)
            {
                hfIdSupervisor.Value = pm.Id.ToString();

                lblPlanMejoramiento.Text = pm.Nombre;

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "abrirModal",
                    @"var modal = new bootstrap.Modal(
                        document.getElementById('modalAsignarSupervisor')
                      );
                      modal.show();",
                    true);
            }
        }

        protected void btnGuardarAsignacion_Click(object sender, EventArgs e)
        {
            if (ddlSupervisorAsignar.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Seleccione un Supervisor');",
                    true);

                return;
            }

            Modelo.PlanMejoramiento asignacion = new Modelo.PlanMejoramiento()
            {
                Id = Convert.ToInt32(ddlSupervisorAsignar.SelectedValue),
                Instructor = new Modelo.Instructor()
                {
                    Id = Convert.ToInt32(hfIdSupervisor.Value)
                }
            };

            bool resultado = oGestor.MtAsignarSupervisorAPlan(asignacion);

            if (resultado)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Ficha asignada correctamente');",
                    true);

                ddlSupervisorAsignar.SelectedIndex = 0;
            }
        }
    }
}
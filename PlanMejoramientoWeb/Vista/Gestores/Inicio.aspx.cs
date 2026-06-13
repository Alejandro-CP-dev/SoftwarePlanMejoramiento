using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Logica;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            UsuarioSesion usuario = Session["Usuario"] as UsuarioSesion;

            gvPlanes.DataSource = oGestor.MtListarPlanMejoramientoPorCentro(usuario.IdCentro);
            gvPlanes.DataBind();
        }

        protected void btnAsignarInstructor_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idPlanMejoramiento = Convert.ToInt32(btn.CommandArgument);

            PlanMejoramiento pm = oPlan.MtObtenerPlanMejoramientoId(idPlanMejoramiento);

            if (pm == null)
            {
                return;
            }

            hfIdSupervisor.Value = pm.Id.ToString();
            lblPlanMejoramiento.Text = pm.Nombre;
            txtIndicacion.Text = "";

            CargarSupervisores(pm.Instructor.Id);

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

        private void CargarSupervisores(int idInstructorCreador)
        {
            // Lista todos los instructores excepto el que creó el plan
            var instructores = oInstructor.MtListarInstructor()
                .Where(i => i.Id != idInstructorCreador)
                .ToList();

            ddlSupervisorAsignar.DataSource = instructores;
            ddlSupervisorAsignar.DataTextField = "Nombre";
            ddlSupervisorAsignar.DataValueField = "Id";
            ddlSupervisorAsignar.DataBind();

            ddlSupervisorAsignar.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
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

            int idPlan = Convert.ToInt32(hfIdSupervisor.Value);
            int idInstructorSupervisor = Convert.ToInt32(ddlSupervisorAsignar.SelectedValue);

            PlanMejoramiento pm = oPlan.MtObtenerPlanMejoramientoId(idPlan);

            if (pm == null)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('No se encontró el plan de mejoramiento');",
                    true);
                return;
            }

            UsuarioSesion usuario = Session["Usuario"] as UsuarioSesion;

            Asignacion asignacion = new Asignacion()
            {
                PlanMejoramiento = new PlanMejoramiento() { Id = idPlan },
                Instructor = new Modelo.Instructor() { Id = idInstructorSupervisor },
                Gestor = new Gestor() { Id = usuario.Id },
                Indicacion = txtIndicacion.Text.Trim()
            };

            string error = oGestor.MtAsignarSupervisorAPlan(asignacion, pm.Instructor.Id);

            if (error != null)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    $"alert('{error}');",
                    true);
                return;
            }

            ClientScript.RegisterStartupScript(
                this.GetType(),
                "mensaje",
                "alert('Supervisor asignado correctamente');",
                true);

            CargarDatos();
        }
    }
}
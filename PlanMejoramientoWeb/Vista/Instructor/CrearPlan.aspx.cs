using PlanMejoramientoWeb.Logica;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Web.UI.WebControls;

namespace PlanMejoramientoWeb.Vista.Instructor
{
    public partial class CrearPlan : System.Web.UI.Page
    {
        private readonly AprendizL oAprendiz = new AprendizL();
        private readonly AprendizPlanL oAprendizPlanL = new AprendizPlanL();
        private readonly PlanMejoramientoL oPlanL = new PlanMejoramientoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                MtCargarAprendices();
        }

        private void MtCargarAprendices()
        {
            UsuarioSesion usuario = Session["Usuario"] as UsuarioSesion;

            var aprendices = oAprendiz.MtListarAprendicesPorInstructor(usuario.Id);

            ddlAprendiz.Items.Clear();
            ddlAprendiz.Items.Add(new ListItem("-- Seleccione un aprendiz --", "0"));

            foreach (var a in aprendices)
            {
                ddlAprendiz.Items.Add(new ListItem(
                    a.Nombre + " " + a.Apellido + " — " + a.NumeroDocumento,
                    a.Id.ToString()));
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlAprendiz.SelectedValue == "0")
            {
                MostrarMensaje("Seleccione un aprendiz.", false); return;
            }
            if (string.IsNullOrWhiteSpace(txtNombrePlan.Text))
            {
                MostrarMensaje("Ingrese el nombre del plan.", false); return;
            }
            if (string.IsNullOrWhiteSpace(txtFechaLimite.Text))
            {
                MostrarMensaje("Seleccione la fecha límite.", false); return;
            }

            UsuarioSesion usuario = Session["Usuario"] as UsuarioSesion;

            // 1 = Interno, 2 = Comité (Disciplinario)
            int idTipoPlan = rbComite.Checked ? 2 : 1;

            PlanMejoramiento plan = new PlanMejoramiento()
            {
                Nombre = txtNombrePlan.Text.Trim(),
                FechaAsignacion = DateTime.Now,
                FechaLimite = Convert.ToDateTime(txtFechaLimite.Text),
                Instructor = new Modelo.Instructor() { Id = usuario.Id },
                TipoPlan = new TipoPlan() { Id = idTipoPlan }
            };

            int idPlan = oPlanL.MtRegistrarPlanMejoramiento(plan);

            if (idPlan > 0)
            {
                AprendizPlan asignacion = new AprendizPlan()
                {
                    Aprendiz = new Modelo.Aprendiz() { Id = Convert.ToInt32(ddlAprendiz.SelectedValue) },
                    PlanMejoramiento = new PlanMejoramiento() { Id = idPlan },
                    Estado = "Activo",
                    Observacion = txtObservacion.Text.Trim(),
                    FechaAsignacion = DateTime.Now
                };

                bool resultado = oAprendizPlanL.MtRegistrarAprendizPlan(asignacion);

                if (resultado)
                {
                    string tipo = idTipoPlan == 2 ? "Por Comité" : "Interno";
                    MostrarMensaje("Plan " + tipo + " creado y asignado correctamente.", true);

                    // Limpiar formulario
                    txtNombrePlan.Text = "";
                    txtFechaLimite.Text = "";
                    txtObservacion.Text = "";
                    rbInterno.Checked = true;
                    rbComite.Checked = false;
                    ddlAprendiz.SelectedIndex = 0;
                }
                else
                {
                    MostrarMensaje("Error al asignar el plan al aprendiz.", false);
                }
            }
            else
            {
                MostrarMensaje("Error al registrar el plan.", false);
            }
        }

        private void MostrarMensaje(string texto, bool exito)
        {
            pnlMensaje.Visible = true;
            string css = exito
                ? "display:block; padding:.75rem 1rem; background:#d1fae5; color:#065f46; border:1px solid #6ee7b7; border-radius:8px;"
                : "display:block; padding:.75rem 1rem; background:#fee2e2; color:#991b1b; border:1px solid #fca5a5; border-radius:8px;";
            lblMensaje.Text = "<div style='" + css + "'>" + texto + "</div>";
        }
    }
}
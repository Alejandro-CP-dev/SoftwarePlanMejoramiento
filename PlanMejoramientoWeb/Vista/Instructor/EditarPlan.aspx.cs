using PlanMejoramientoWeb.Logica;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Web.UI.WebControls;

namespace PlanMejoramientoWeb.Vista.Instructor
{
    public partial class EditarPlan : System.Web.UI.Page
    {
        private readonly PlanMejoramientoL oPlanL = new PlanMejoramientoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarPlanes();
                MtPreseleccionarDesdeQueryString();
            }
        }

        private void MtPreseleccionarDesdeQueryString()
        {
            string idPlanQs = Request.QueryString["idPlan"];
            if (string.IsNullOrEmpty(idPlanQs)) return;

            ListItem item = ddlPlanes.Items.FindByValue(idPlanQs);
            if (item != null)
            {
                ddlPlanes.ClearSelection();
                item.Selected = true;
                ddlPlanes_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        private void MtCargarPlanes()
        {
            UsuarioSesion usuario = Session["Usuario"] as UsuarioSesion;
            var planes = oPlanL.MtListarPlanesPorInstructor(usuario.Id);

            ddlPlanes.Items.Clear();
            ddlPlanes.Items.Add(new ListItem("-- Seleccione un plan --", "0"));

            foreach (var p in planes)
            {
                string texto = string.Format("[{0}] {1} — Límite: {2}",
                    p.TipoPlan.Nombre,
                    p.Nombre,
                    p.FechaLimite.ToString("dd/MM/yyyy"));

                ddlPlanes.Items.Add(new ListItem(texto, p.Id.ToString()));
            }

            pnlEditar.Visible = false;
        }

        protected void ddlPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMensaje.Visible = false;

            if (ddlPlanes.SelectedValue == "0")
            {
                pnlEditar.Visible = false;
                return;
            }

            int idPlan = Convert.ToInt32(ddlPlanes.SelectedValue);
            PlanMejoramiento plan = oPlanL.MtObtenerPlanMejoramientoId(idPlan);

            if (plan == null)
            {
                pnlEditar.Visible = false;
                return;
            }

            hfIdPlan.Value = plan.Id.ToString();
            txtNombre.Text = plan.Nombre;
            txtFechaLimite.Text = plan.FechaLimite.ToString("yyyy-MM-dd");
            txtObservacion.Text = "";

            lblTipo.Text = plan.TipoPlan.BadgeHtml;

            pnlEditar.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarMensaje("Ingrese el nombre del plan.", false); return;
            }
            if (string.IsNullOrWhiteSpace(txtFechaLimite.Text))
            {
                MostrarMensaje("Seleccione la fecha límite.", false); return;
            }

            int idPlan = Convert.ToInt32(hfIdPlan.Value);

            PlanMejoramiento plan = new PlanMejoramiento()
            {
                Id = idPlan,
                Nombre = txtNombre.Text.Trim(),
                FechaLimite = Convert.ToDateTime(txtFechaLimite.Text)
            };

            bool okPlan = oPlanL.MtActualizarPlanMejoramiento(plan);

            // Actualizar observación si se ingresó
            if (!string.IsNullOrWhiteSpace(txtObservacion.Text))
                oPlanL.MtActualizarObservacionPlan(idPlan, txtObservacion.Text.Trim());

            if (okPlan)
            {
                MostrarMensaje("Plan actualizado correctamente.", true);
                MtCargarPlanes();
            }
            else
            {
                MostrarMensaje("Error al actualizar el plan.", false);
            }
        }

        private void MostrarMensaje(string texto, bool exito)
        {
            pnlMensaje.Visible = true;
            string css = exito
                ? "display:block;padding:.75rem 1rem;background:#d1fae5;color:#065f46;border:1px solid #6ee7b7;border-radius:8px;"
                : "display:block;padding:.75rem 1rem;background:#fee2e2;color:#991b1b;border:1px solid #fca5a5;border-radius:8px;";
            lblMensaje.Text = "<div style='" + css + "'>" + texto + "</div>";
        }
    }
}
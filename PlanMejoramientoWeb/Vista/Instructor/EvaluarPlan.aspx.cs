using PlanMejoramientoWeb.Logica;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Web.UI.WebControls;

namespace PlanMejoramientoWeb.Vista.Instructor
{
    public partial class EvaluarPlan : System.Web.UI.Page
    {
        private readonly AprendizPlanL oAprendizPlanL = new AprendizPlanL();
        private readonly EvaluacionPlanL oEvaluacionL = new EvaluacionPlanL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarPlanes();
                MtPreseleccionarDesdeQueryString();
            }
        }

        // Value del item: "IdAprendizPlan|IdPlanMejoramiento|IdAprendiz|IdTipoPlan"
        private void MtCargarPlanes()
        {
            UsuarioSesion usuario = Session["Usuario"] as UsuarioSesion;
            var planes = oAprendizPlanL.MtListarPlanesPorInstructor(usuario.Id);

            ddlPlanes.Items.Clear();
            ddlPlanes.Items.Add(new ListItem("-- Seleccione un plan --", "0"));

            foreach (var ap in planes)
            {
                string texto = string.Format("[{0}] {1} {2} — {3} (Límite: {4})",
                    ap.PlanMejoramiento.TipoPlan.Nombre,
                    ap.Aprendiz.Nombre,
                    ap.Aprendiz.Apellido,
                    ap.PlanMejoramiento.Nombre,
                    ap.PlanMejoramiento.FechaLimite.ToString("dd/MM/yyyy"));

                string valor = string.Join("|",
                    ap.Id,
                    ap.PlanMejoramiento.Id,
                    ap.Aprendiz.Id,
                    ap.PlanMejoramiento.TipoPlan.Id);

                ddlPlanes.Items.Add(new ListItem(texto, valor));
            }

            pnlEvaluar.Visible = false;
            pnlResultado.Visible = false;
        }

        private void MtPreseleccionarDesdeQueryString()
        {
            string idPlanQs = Request.QueryString["idPlan"];
            if (string.IsNullOrEmpty(idPlanQs)) return;

            foreach (ListItem item in ddlPlanes.Items)
            {
                string[] partes = item.Value.Split('|');
                if (partes.Length >= 2 && partes[1] == idPlanQs)
                {
                    ddlPlanes.ClearSelection();
                    item.Selected = true;
                    ddlPlanes_SelectedIndexChanged(this, EventArgs.Empty);
                    break;
                }
            }
        }

        protected void ddlPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlResultado.Visible = false;

            if (ddlPlanes.SelectedValue == "0")
            {
                pnlEvaluar.Visible = false;
                return;
            }

            string[] partes = ddlPlanes.SelectedValue.Split('|');
            int idTipoPlan = Convert.ToInt32(partes[3]);

            lblNombrePlan.Text = ddlPlanes.SelectedItem.Text;
            lblBadgeTipo.Text = oEvaluacionL.MtEsPlanComite(idTipoPlan)
                ? "<span class='badge-comite'>Por Comité</span>"
                : "<span class='badge-interno'>Interno</span>";

            rbProductoAprueba.Checked = false;
            rbProductoNoAprueba.Checked = false;
            rbConocimientoAprueba.Checked = false;
            rbConocimientoNoAprueba.Checked = false;
            rbDesempenoAprueba.Checked = false;
            rbDesempenoNoAprueba.Checked = false;

            pnlEvaluar.Visible = true;
        }

        protected void btnEvaluar_Click(object sender, EventArgs e)
        {
            if (!rbProductoAprueba.Checked && !rbProductoNoAprueba.Checked)
            {
                MostrarResultado("Debe seleccionar el criterio de Producto.", "error"); return;
            }
            if (!rbConocimientoAprueba.Checked && !rbConocimientoNoAprueba.Checked)
            {
                MostrarResultado("Debe seleccionar el criterio de Conocimiento.", "error"); return;
            }
            if (!rbDesempenoAprueba.Checked && !rbDesempenoNoAprueba.Checked)
            {
                MostrarResultado("Debe seleccionar el criterio de Desempeño.", "error"); return;
            }

            string[] partes = ddlPlanes.SelectedValue.Split('|');
            int idPlan = Convert.ToInt32(partes[1]);

            UsuarioSesion usuario = Session["Usuario"] as UsuarioSesion;

            EvaluacionPlan evaluacion = new EvaluacionPlan()
            {
                EvaluacionProducto = rbProductoAprueba.Checked ? "Aprueba" : "No Aprueba",
                EvaluacionConocimiento = rbConocimientoAprueba.Checked ? "Aprueba" : "No Aprueba",
                EvaluacionDesempeno = rbDesempenoAprueba.Checked ? "Aprueba" : "No Aprueba",
                PlanMejoramiento = new PlanMejoramiento() { Id = idPlan }
            };

            ResultadoEvaluacion resultado = oEvaluacionL.MtProcesarEvaluacion(evaluacion, usuario.Id);

            string tipoVisual = oEvaluacionL.MtNombreTipoResultado(resultado.Tipo);
            MostrarResultado(resultado.Mensaje, tipoVisual);

            MtCargarPlanes();
        }

        private void MostrarResultado(string mensaje, string tipo)
        {
            pnlResultado.Visible = true;
            lblResultado.Text = mensaje;
            divResultado.Style["display"] = "block";
            divResultado.Attributes["class"] = tipo == "exito" ? "result-box result-ok" :
                                               tipo == "alerta" ? "result-box result-alerta" :
                                                                  "result-box result-err";
        }
    }
}
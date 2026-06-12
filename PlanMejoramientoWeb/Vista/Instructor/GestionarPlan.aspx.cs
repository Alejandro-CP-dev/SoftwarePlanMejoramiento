using PlanMejoramientoWeb.Logica;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Web.UI.WebControls;

namespace PlanMejoramientoWeb.Vista.Instructor
{
    public partial class GestionarPlan : System.Web.UI.Page
    {
        private readonly AprendizL oAprendizL = new AprendizL();
        private readonly AprendizPlanL oAprendizPlanL = new AprendizPlanL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                MtCargarDatos();
        }

        private int MtObtenerIdAprendiz()
        {
            int idAprendiz;
            if (!int.TryParse(Request.QueryString["idAprendiz"], out idAprendiz))
            {
                Response.Redirect("Aprendices.aspx");
            }
            return idAprendiz;
        }

        private void MtCargarDatos()
        {
            int idAprendiz = MtObtenerIdAprendiz();

            // Cabecera con datos del aprendiz
            Modelo.Aprendiz aprendiz = oAprendizL.MtObtenerAprendizPorId(idAprendiz);

            if (aprendiz == null)
            {
                Response.Redirect("Aprendices.aspx");
                return;
            }

            lblNombreCompleto.Text = aprendiz.Nombre + " " + aprendiz.Apellido;
            lblDocumento.Text = aprendiz.NumeroDocumento;
            lblCorreo.Text = aprendiz.Correo;

            // Iniciales para el avatar
            string ini1 = aprendiz.Nombre.Length > 0 ? aprendiz.Nombre[0].ToString() : "";
            string ini2 = aprendiz.Apellido.Length > 0 ? aprendiz.Apellido[0].ToString() : "";
            lblIniciales.Text = (ini1 + ini2).ToUpper();

            // Badge de estado académico (la clasificación vive en el modelo EstadoAcademico)
            if (aprendiz.EstadoAcademico != null)
            {
                lblEstadoAcademico.Text = aprendiz.EstadoAcademico.Nombre;
                lblEstadoAcademico.CssClass = "badge-estado " + aprendiz.EstadoAcademico;
            }
            else
            {
                lblEstadoAcademico.Text = "";
                lblEstadoAcademico.CssClass = "badge-estado badge-otro";
            }

            // Lista de planes
            var planes = oAprendizPlanL.MtListarPlanesPorAprendiz(idAprendiz);

            if (planes.Count == 0)
            {
                pnlSinPlanes.Visible = true;
                rptPlanes.Visible = false;
            }
            else
            {
                rptPlanes.DataSource = planes;
                rptPlanes.DataBind();
            }
        }

        protected void rptPlanes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idPlan = Convert.ToInt32(e.CommandArgument);
            int idAprendiz = MtObtenerIdAprendiz();

            if (e.CommandName == "Editar")
            {
                Response.Redirect("EditarPlan.aspx?idPlan=" + idPlan + "&idAprendiz=" + idAprendiz);
            }
            else if (e.CommandName == "Evaluar")
            {
                Response.Redirect("EvaluarPlan.aspx?idPlan=" + idPlan + "&idAprendiz=" + idAprendiz);
            }
        }
    }
}
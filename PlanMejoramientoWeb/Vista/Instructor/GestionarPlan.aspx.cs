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
    public partial class GestionarPlan : System.Web.UI.Page
    {
        private AprendizL oAprendiz = new AprendizL();
        private PlanMejoramientoL oPlan = new PlanMejoramientoL();
        private AprendizPlanL oAprendizPlan = new AprendizPlanL();
        private FichaAprendizL oFichaAprendiz = new FichaAprendizL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["idAprendiz"] != null)
                {
                    int idAprendiz =
                        Convert.ToInt32(Request.QueryString["idAprendiz"]);

                    hfIdAprendiz.Value = idAprendiz.ToString();

                    MtCargarAprendiz(idAprendiz);

                    MtCargarFicha(idAprendiz);

                    MtCargarPlan(idAprendiz);
                }
            }
        }

        public void MtCargarAprendiz(int idAprendiz)
        {
            Modelo.Aprendiz aprendiz = oAprendiz.MtObtenerAprendizPorId(idAprendiz);

            if (aprendiz != null)
            {
                txtDocumento.Text = aprendiz.NumeroDocumento;

                txtNombre.Text = aprendiz.Nombre + " " + aprendiz.Apellido;

                txtEstado.Text = aprendiz.EstadoAcademico.Nombre;
            }
        }

        public void MtCargarFicha(int idAprendiz)
        {
            var lista =
                oFichaAprendiz.MtListarFichaPorAprendiz(idAprendiz);

            if (lista.Count > 0)
            {
                txtFicha.Text =
                    lista[0].CodigoFicha;
            }
        }

        public void MtCargarPlan(int idAprendiz)
        {
            PlanMejoramiento plan =
                oAprendizPlan.MtObtenerPlanActivo(idAprendiz);

            if (plan == null)
            {
                pnlCrearPlan.Visible = true;

                pnlPlanExistente.Visible = false;

                return;
            }

            hfIdPlan.Value =
                plan.Id.ToString();

            txtPlan.Text =
                plan.Nombre;

            txtTipoPlan.Text =
                plan.TipoPlan.Nombre;

            txtFechaAsignacion.Text =
                plan.FechaAsignacion.ToShortDateString();

            txtFechaLimitePlan.Text =
                plan.FechaLimite.ToShortDateString();

            pnlCrearPlan.Visible = false;

            pnlPlanExistente.Visible = true;
        }

        protected void btnCrearPlan_Click(object sender, EventArgs e)
        {
            UsuarioSesion usuario = Session["Usuario"] as UsuarioSesion;

            PlanMejoramiento plan = new PlanMejoramiento()
            {
                Nombre = txtNombrePlan.Text,

                FechaAsignacion = DateTime.Now,

                FechaLimite =
                Convert.ToDateTime(txtFechaLimite.Text),

                TipoPlan = new TipoPlan()
                {
                    Id = 1
                },

                Instructor = new Modelo.Instructor()
                {
                    Id = usuario.Id
                }
            };

            int idPlan = oPlan.MtRegistrarPlanMejoramiento(plan);

            if (idPlan > 0)
            {
                AprendizPlan aprendizPlan = new AprendizPlan()
                {
                    Aprendiz = new Modelo.Aprendiz()
                    {
                        Id = Convert.ToInt32(hfIdAprendiz.Value)
                    },

                    PlanMejoramiento = new PlanMejoramiento()
                    {
                        Id = idPlan
                    },

                    Estado = "Activo",

                    Observacion = txtObservacion.Text,

                    FechaAsignacion = DateTime.Now
                };

                bool resultado = oAprendizPlan.MtRegistrarAprendizPlan(aprendizPlan);

                if (resultado)
                {
                    MtCargarPlan(
                        Convert.ToInt32(hfIdAprendiz.Value));

                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "mensaje",
                        "alert('Plan creado correctamente');",
                        true);
                }
            }
        }
    }
}
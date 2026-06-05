using PlanMejoramientoWeb.Datos;
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
    public partial class CrearPlan : System.Web.UI.Page
    {
        private AprendizL oAprendiz = new AprendizL();
        private AprendizPlanL oAprendizPlan = new AprendizPlanL();
        private PlanMejoramientoD oPlanMejoramiento = new PlanMejoramientoD();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarAprendices();
            }
        }

        public void MtCargarAprendices()
        {
            UsuarioSesion usuario = Session["Usuario"] as UsuarioSesion;

            ddlAprendiz.DataSource = oAprendiz.MtListarAprendicesPorInstructor(usuario.Id);

            ddlAprendiz.DataTextField = "Nombre" + "" + "Apellido";
            ddlAprendiz.DataValueField = "Id";
            ddlAprendiz.DataBind();

            ddlAprendiz.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlAprendiz.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Seleccione un aprendiz');",
                    true);

                return;
            }

            Modelo.PlanMejoramiento plan =
                new Modelo.PlanMejoramiento()
                {
                    Nombre = txtNombrePlan.Text,

                    FechaAsignacion = DateTime.Now,

                    FechaLimite =
                        Convert.ToDateTime(
                            txtFechaLimite.Text),

                    TipoPlan = new Modelo.TipoPlan()
                    {
                        Id = 1 // Interno
                    }
                };

            int idPlan =
                oPlanMejoramiento
                .MtRegistrarPlanMejoramiento(plan);

            if (idPlan > 0)
            {
                Modelo.AprendizPlan asignacion =
                    new Modelo.AprendizPlan()
                    {
                        Aprendiz =
                            new Modelo.Aprendiz()
                            {
                                Id = Convert.ToInt32(ddlAprendiz.SelectedValue)
                            },

                        PlanMejoramiento = new Modelo.PlanMejoramiento()
                        {
                            Id = idPlan
                        },

                        Estado = "Activo",

                        Observacion = txtObservacion.Text,

                        FechaAsignacion = DateTime.Now
                    };

                bool resultado = oAprendizPlan.MtRegistrarAprendizPlan(asignacion);

                if (resultado)
                {
                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "mensaje",
                        "alert('Plan de mejoramiento creado correctamente');",
                        true);
                }
            }
        }
    }
}
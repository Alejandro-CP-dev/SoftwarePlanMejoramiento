using PlanMejoramientoWeb.Logica;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramientoWeb.Vista.Administrador
{
    public partial class Ficha : System.Web.UI.Page
    {
        private FichaL oFicha = new FichaL();
        private ProgramaL oPrograma = new ProgramaL();
        private JornadaL oJornada = new JornadaL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarProgramas();
                MtCargarJornadas();
                MtCargarFichas();

                btnActualizar.Visible = false;
            }
        }

        public void MtCargarProgramas()
        {
            ddlPrograma.DataSource = oPrograma.MtListarPrograma();
            ddlPrograma.DataTextField = "Nombre";
            ddlPrograma.DataValueField = "Id";
            ddlPrograma.DataBind();

            ddlPrograma.Items.Insert(0,
                new ListItem("-- Seleccione --", "0"));
        }

        public void MtCargarJornadas()
        {
            ddlJornada.DataSource = oJornada.MtListarJornada();
            ddlJornada.DataTextField = "Nombre";
            ddlJornada.DataValueField = "Id";
            ddlJornada.DataBind();

            ddlJornada.Items.Insert(0,
                new ListItem("-- Seleccione --", "0"));
        }

        public void MtCargarFichas()
        {
            gvFichas.DataSource = oFicha.MtListarFicha();
            gvFichas.DataBind();
        }

        public void MtLimpiarFormulario()
        {
            hfIdFicha.Value = "";

            txtCodigoFicha.Text = "";
            txtFechaInicio.Text = "";
            txtFechaFinalizacion.Text = "";
            txtDescripcion.Text = "";

            ddlPrograma.SelectedIndex = 0;
            ddlJornada.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;

            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Modelo.Ficha ficha = new Modelo.Ficha()
            {
                CodigoFicha = txtCodigoFicha.Text,
                FechaInicio = Convert.ToDateTime(txtFechaInicio.Text),
                FechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacion.Text),
                Descripcion = txtDescripcion.Text,
                Estado = ddlEstado.SelectedValue,

                Programa = new Programa()
                {
                    Id = Convert.ToInt32(ddlPrograma.SelectedValue)
                },

                Jornada = new Jornada()
                {
                    Id = Convert.ToInt32(ddlJornada.SelectedValue)
                }
            };

            bool resultado = oFicha.MtRegistrarFicha(ficha);

            if (resultado)
            {
                MtCargarFichas();
                MtLimpiarFormulario();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Ficha registrada correctamente');",
                    true);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            MtLimpiarFormulario();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Modelo.Ficha ficha = new Modelo.Ficha()
            {
                Id = Convert.ToInt32(hfIdFicha.Value),
                CodigoFicha = txtCodigoFicha.Text,
                FechaInicio = Convert.ToDateTime(txtFechaInicio.Text),
                FechaFinalizacion = Convert.ToDateTime(txtFechaFinalizacion.Text),
                Descripcion = txtDescripcion.Text,
                Estado = ddlEstado.SelectedValue,

                Programa = new Programa()
                {
                    Id = Convert.ToInt32(ddlPrograma.SelectedValue)
                },

                Jornada = new Jornada()
                {
                    Id = Convert.ToInt32(ddlJornada.SelectedValue)
                }
            };

            bool resultado = oFicha.MtActualizarFicha(ficha);

            if (resultado)
            {
                MtCargarFichas();
                MtLimpiarFormulario();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Ficha actualizada correctamente');",
                    true);
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int id = Convert.ToInt32(btn.CommandArgument);

            Modelo.Ficha ficha = oFicha.MtObtenerFichaPorId(id);

            if (ficha != null)
            {
                hfIdFicha.Value = ficha.Id.ToString();

                txtCodigoFicha.Text = ficha.CodigoFicha;
                txtFechaInicio.Text = ficha.FechaInicio.ToString("yyyy-MM-dd");
                txtFechaFinalizacion.Text = ficha.FechaFinalizacion.ToString("yyyy-MM-dd");
                txtDescripcion.Text = ficha.Descripcion;

                ddlEstado.SelectedValue = ficha.Estado;
                ddlPrograma.SelectedValue = ficha.Programa.Id.ToString();
                ddlJornada.SelectedValue = ficha.Jornada.Id.ToString();

                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int id = Convert.ToInt32(btn.CommandArgument);

            bool resultado = oFicha.MtEliminarFicha(id);

            if (resultado)
            {
                MtCargarFichas();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Ficha eliminada correctamente');",
                    true);
            }
        }
    }
}
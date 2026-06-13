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

        private void MtMostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "mensaje",
                "alert('" + mensaje.Replace("'", "\\'") + "');",
                true);
        }

   
        private Modelo.Ficha MtConstruirFichaDesdeFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtFechaInicio.Text) ||
                string.IsNullOrWhiteSpace(txtFechaFinalizacion.Text))
            {
                MtMostrarMensaje("Debe ingresar las fechas de inicio y finalización.");
                return null;
            }

            if (!DateTime.TryParse(txtFechaInicio.Text, out DateTime fechaInicio) ||
                !DateTime.TryParse(txtFechaFinalizacion.Text, out DateTime fechaFin))
            {
                MtMostrarMensaje("Las fechas ingresadas no son válidas.");
                return null;
            }

            if (ddlPrograma.SelectedValue == "0")
            {
                MtMostrarMensaje("Debe seleccionar un programa.");
                return null;
            }

            if (ddlJornada.SelectedValue == "0")
            {
                MtMostrarMensaje("Debe seleccionar una jornada.");
                return null;
            }

            return new Modelo.Ficha()
            {
                CodigoFicha = txtCodigoFicha.Text.Trim(),
                FechaInicio = fechaInicio,
                FechaFinalizacion = fechaFin,
                Descripcion = txtDescripcion.Text.Trim(),
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
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Modelo.Ficha ficha = MtConstruirFichaDesdeFormulario();
            if (ficha == null)
            {
                return;
            }

            string error = oFicha.MtRegistrarFicha(ficha);

            if (error == null)
            {
                MtCargarFichas();
                MtLimpiarFormulario();
                MtMostrarMensaje("Ficha registrada correctamente");
            }
            else
            {
                MtMostrarMensaje(error);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Modelo.Ficha ficha = MtConstruirFichaDesdeFormulario();
            if (ficha == null)
            {
                return;
            }

            ficha.Id = Convert.ToInt32(hfIdFicha.Value);

            string error = oFicha.MtActualizarFicha(ficha);

            if (error == null)
            {
                MtCargarFichas();
                MtLimpiarFormulario();
                MtMostrarMensaje("Ficha actualizada correctamente");
            }
            else
            {
                MtMostrarMensaje(error);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            MtLimpiarFormulario();
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

            string error = oFicha.MtEliminarFicha(id);

            if (error == null)
            {
                MtCargarFichas();
                MtMostrarMensaje("Ficha eliminada correctamente");
            }
            else
            {
                MtMostrarMensaje(error);
            }
        }
    }
}
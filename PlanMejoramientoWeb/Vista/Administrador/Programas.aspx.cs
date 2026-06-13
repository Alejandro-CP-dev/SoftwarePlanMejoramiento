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
    public partial class Programas : System.Web.UI.Page
    {
        private ProgramaL oPrograma = new ProgramaL();
        private NivelFormacionL oNivelFormacion = new NivelFormacionL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarNiveles();
                MtCargarProgramas();

                btnActualizar.Visible = false;
            }
        }

        public void MtCargarNiveles()
        {
            ddlNivelFormacion.DataSource = oNivelFormacion.MtListarNivel();
            ddlNivelFormacion.DataTextField = "Nombre";
            ddlNivelFormacion.DataValueField = "Id";

            ddlNivelFormacion.DataBind();

            ddlNivelFormacion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }

        public void MtCargarProgramas()
        {
            gvProgramas.DataSource = oPrograma.MtListarPrograma();
            gvProgramas.DataBind();
        }

        public void MtLimpiarFormulario()
        {
            hfIdPrograma.Value = "";

            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtVersion.Text = "";
            txtDuracion.Text = "";

            ddlNivelFormacion.SelectedIndex = 0;
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

        private Programa MtConstruirProgramaDesdeFormulario()
        {
            if (ddlNivelFormacion.SelectedValue == "0")
            {
                MtMostrarMensaje("Debe seleccionar un nivel de formación.");
                return null;
            }

            if (!int.TryParse(txtDuracion.Text, out int duracion))
            {
                MtMostrarMensaje("La duración debe ser un número válido.");
                return null;
            }

            return new Programa
            {
                Codigo = txtCodigo.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Version = txtVersion.Text.Trim(),
                Duracion = duracion,
                Estado = ddlEstado.SelectedValue,
                NivelFormacion = new NivelFormacion()
                {
                    Id = Convert.ToInt32(ddlNivelFormacion.SelectedValue)
                }
            };
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Programa programa = MtConstruirProgramaDesdeFormulario();
            if (programa == null)
            {
                return;
            }

            string error = oPrograma.MtRegistrarPrograma(programa);

            if (error == null)
            {
                MtCargarProgramas();
                MtLimpiarFormulario();
                MtMostrarMensaje("Programa registrado correctamente");
            }
            else
            {
                MtMostrarMensaje(error);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Programa programa = MtConstruirProgramaDesdeFormulario();
            if (programa == null)
            {
                return;
            }

            programa.Id = Convert.ToInt32(hfIdPrograma.Value);

            string error = oPrograma.MtActualizarPrograma(programa);

            if (error == null)
            {
                MtCargarProgramas();
                MtLimpiarFormulario();
                MtMostrarMensaje("Programa actualizado correctamente");
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

            Programa programa = oPrograma.MtObtenerProgramaPorId(id);

            if (programa != null)
            {
                hfIdPrograma.Value = programa.Id.ToString();
                txtCodigo.Text = programa.Codigo;
                txtNombre.Text = programa.Nombre;
                txtVersion.Text = programa.Version;
                txtDuracion.Text = programa.Duracion.ToString();

                ddlEstado.SelectedValue = programa.Estado;
                ddlNivelFormacion.SelectedValue = programa.NivelFormacion.Id.ToString();

                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int id = Convert.ToInt32(btn.CommandArgument);

            string error = oPrograma.MtEliminarPrograma(id);

            if (error == null)
            {
                MtCargarProgramas();
                MtMostrarMensaje("Programa eliminado correctamente");
            }
            else
            {
                MtMostrarMensaje(error);
            }
        }
    }
}
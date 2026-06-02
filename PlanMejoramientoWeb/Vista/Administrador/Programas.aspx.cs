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


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Programa programa = new Programa
            {
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Version = txtVersion.Text,
                Duracion = Convert.ToInt32(txtDuracion.Text),
                Estado = ddlEstado.SelectedValue,
                NivelFormacion = new NivelFormacion()
                {
                    Id = Convert.ToInt32(ddlNivelFormacion.SelectedValue)
                }
            };

            bool resultado = oPrograma.MtRegistrarPrograma(programa);

            if (resultado)
            {
                MtCargarProgramas();
                MtLimpiarFormulario();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Programa registrado correctamente');",
                    true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Programa programa = new Programa()
            {
                Id = Convert.ToInt32(hfIdPrograma.Value),
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Version = txtVersion.Text,
                Duracion = Convert.ToInt32(txtDuracion.Text),
                Estado = ddlEstado.SelectedValue,
                NivelFormacion = new NivelFormacion()
                {
                    Id = Convert.ToInt32(ddlNivelFormacion.SelectedValue)
                }
            };

            bool resultado = oPrograma.MtActualizarPrograma(programa);

            if (resultado)
            {
                MtCargarProgramas();
                MtLimpiarFormulario();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Programa Actualizado correctamente');",
                    true);
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

            bool resultado = oPrograma.MtEliminarPrograma(id);

            if (resultado)
            {
                MtCargarProgramas();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Programa eliminado correctamente');",
                    true);
            }
        }
    }
}
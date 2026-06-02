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
    public partial class Aprendices : System.Web.UI.Page
    {
        private AprendizL oAprendiz = new AprendizL();
        private EstadoAcademicoL oEstadoAcademico = new EstadoAcademicoL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarEstadoAcademico();
                MtCargarAprendices();

                btnActualizar.Visible = false;
            }
        }

        public void MtCargarEstadoAcademico()
        {
            ddlEstadoAcademico.DataSource = oEstadoAcademico.MtListarEstadoAcademico();
            ddlEstadoAcademico.DataTextField = "Nombre";
            ddlEstadoAcademico.DataValueField = "Id";
            ddlEstadoAcademico.DataBind();

            ddlEstadoAcademico.Items.Insert(0,
                new ListItem("-- Seleccione --", "0"));
        }

        public void MtCargarAprendices()
        {
            gvAprendices.DataSource = oAprendiz.MtListarAprendiz();
        }

        public void MtLimpiarFormulario()
        {
            hfIdAprendiz.Value = "";

            ddlTipoDocumento.SelectedIndex = 0;

            txtNumeroDocumento.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            txtContrasena.Text = "";

            ddlEstadoAcademico.SelectedIndex = 0;

            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Modelo.Aprendiz aprendiz = new Modelo.Aprendiz()
            {
                TipoDocumento = ddlTipoDocumento.SelectedValue,
                NumeroDocumento = txtNumeroDocumento.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Correo = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Contrasena = txtContrasena.Text,

                EstadoAcademico = new EstadoAcademico()
                {
                    Id = Convert.ToInt32(
                        ddlEstadoAcademico.SelectedValue)
                }

            };

            bool resultado = oAprendiz.MtRegistrarAprendiz(aprendiz);

            if (resultado)
            {
                MtCargarAprendices();
                MtLimpiarFormulario();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Aprendiz registrado correctamente');",
                    true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Modelo.Aprendiz aprendiz = new Modelo.Aprendiz()
            {
                Id = Convert.ToInt32(hfIdAprendiz.Value),

                TipoDocumento = ddlTipoDocumento.SelectedValue,
                NumeroDocumento = txtNumeroDocumento.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Correo = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Contrasena = txtContrasena.Text,

                EstadoAcademico = new EstadoAcademico()
                {
                    Id = Convert.ToInt32(
                        ddlEstadoAcademico.SelectedValue)
                }
            };

            bool resultado =
                oAprendiz.MtActualizarAprendiz(aprendiz);

            if (resultado)
            {
                MtCargarAprendices();
                MtLimpiarFormulario();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Aprendiz actualizado correctamente');",
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

            int id =
                Convert.ToInt32(btn.CommandArgument);

            Modelo.Aprendiz aprendiz = oAprendiz.MtObtenerAprendizPorId(id);

            if (aprendiz != null)
            {
                hfIdAprendiz.Value = aprendiz.Id.ToString();
                ddlTipoDocumento.SelectedValue = aprendiz.TipoDocumento;
                txtNumeroDocumento.Text = aprendiz.NumeroDocumento;
                txtNombre.Text = aprendiz.Nombre;
                txtApellido.Text = aprendiz.Apellido;
                txtCorreo.Text = aprendiz.Correo;
                txtTelefono.Text = aprendiz.Telefono;
                txtContrasena.Text = aprendiz.Contrasena;
                ddlEstadoAcademico.SelectedValue = aprendiz.EstadoAcademico.Id.ToString();

                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int id =
                Convert.ToInt32(btn.CommandArgument);

            bool resultado =
                oAprendiz.MtEliminarAprendiz(id);

            if (resultado)
            {
                MtCargarAprendices();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Aprendiz eliminado correctamente');",
                    true);
            }
        }
    }
}
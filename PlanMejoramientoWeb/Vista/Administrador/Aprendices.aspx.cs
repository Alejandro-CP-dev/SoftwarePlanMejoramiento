using PlanMejoramientoWeb.Datos;
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
        private FichaL oFicha = new FichaL();
        private FichaAprendizL oFichaAprendiz = new FichaAprendizL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarEstadoAcademico();
                MtCargarAprendices();
                MtCargarFichasAsignar();

                btnActualizar.Visible = false;

            }
            ddlEstadoAcademico.Visible = false;
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
            gvAprendices.DataBind();
        }

        public void MtCargarFichasAsignar()
        {
            ddlFichaAsignar.DataSource = oFicha.MtListarFicha();
            ddlFichaAsignar.DataTextField = "CodigoFicha";
            ddlFichaAsignar.DataValueField = "Id";
            ddlFichaAsignar.DataBind();

            ddlFichaAsignar.Items.Insert(
                0,
                new ListItem("-- Seleccione --", "0"));
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
                    Id = 1
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
                    Id = Convert.ToInt32(ddlEstadoAcademico.SelectedValue)
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
                ddlEstadoAcademico.Visible = true;
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

        protected void btnAsignarFicha_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int idAprendiz = Convert.ToInt32(btn.CommandArgument);

            Modelo.Aprendiz aprendiz =
                oAprendiz.MtObtenerAprendizPorId(idAprendiz);

            if (aprendiz != null)
            {
                hfIdAprendiz.Value = aprendiz.Id.ToString();

                lblAprendiz.Text =
                    aprendiz.Nombre + " " + aprendiz.Apellido;

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "abrirModal",
                    @"var modal = new bootstrap.Modal(
                document.getElementById('modalAsignarFicha')
              );
              modal.show();",
                    true);
            }
        }

        protected void btnGuardarAsignacion_Click(object sender, EventArgs e)
        {
            if (ddlFichaAsignar.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Seleccione una ficha');",
                    true);

                return;
            }

            Modelo.FichaAprendiz asignacion =
                new Modelo.FichaAprendiz()
                {
                    Ficha = new Modelo.Ficha()
                    {
                        Id = Convert.ToInt32(
                            ddlFichaAsignar.SelectedValue)
                    },

                    Aprendiz = new Modelo.Aprendiz()
                    {
                        Id = Convert.ToInt32(
                            hfIdAprendiz.Value)
                    }
                };

            bool resultado =
                oFichaAprendiz.MtRegistrarFichaAprendiz(asignacion);

            if (resultado)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Ficha asignada correctamente');",
                    true);

                ddlFichaAsignar.SelectedIndex = 0;
            }
        }

        protected void btnVerFichas_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int idAprendiz = Convert.ToInt32(btn.CommandArgument);

            Modelo.Aprendiz aprendiz = oAprendiz.MtObtenerAprendizPorId(idAprendiz);

            lblNombreAprendiz.Text = aprendiz.Nombre + " " + aprendiz.Apellido;

            gvFichasAprendiz.DataSource = oFichaAprendiz.MtListarFichaPorAprendiz(idAprendiz);

            gvFichasAprendiz.DataBind();

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "abrirModal",
                @"var modal = new bootstrap.Modal(
            document.getElementById('modalVerFichas')
          );
          modal.show();",
                true);
        }
    }
}
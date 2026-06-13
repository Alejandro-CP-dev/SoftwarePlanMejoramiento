using PlanMejoramientoWeb.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanMejoramientoWeb.Vista.Administrador
{
    public partial class Instructores : System.Web.UI.Page
    {
        private InstructorL oInstructor = new InstructorL();
        private FichaL oFicha = new FichaL();
        private FichaInstructorL oFichaInstructor = new FichaInstructorL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarInstructores();
                MtCargarFichasAsignar();
                btnActualizar.Visible = false;

            }
        }

        public void MtCargarInstructores()
        {
            gvInstructores.DataSource = oInstructor.MtListarInstructor();
            gvInstructores.DataBind();
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
            hfIdInstructor.Value = "";

            ddlTipoDocumento.SelectedIndex = 0;
            txtNumeroDocumento.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            txtContrasena.Text = "";
            ddlEstado.SelectedIndex = 0;

            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int id = Convert.ToInt32(btn.CommandArgument);

            bool resultado = oInstructor.MtEliminarInstructor(id);

            if (resultado)
            {
                MtCargarInstructores();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Instructor eliminado correctamente');",
                    true);
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int id = Convert.ToInt32(btn.CommandArgument);

            Modelo.Instructor instructor = oInstructor.MtObtenerInstructorPorId(id);

            if (instructor != null)
            {
                hfIdInstructor.Value = instructor.Id.ToString();

                ddlTipoDocumento.SelectedValue = instructor.TipoDocumento;
                txtNumeroDocumento.Text = instructor.NumeroDocumento;
                txtNombre.Text = instructor.Nombre;
                txtApellido.Text = instructor.Apellido;
                txtCorreo.Text = instructor.Correo;
                txtTelefono.Text = instructor.Telefono;
                txtContrasena.Text = instructor.Contrasena;
                ddlEstado.SelectedValue = instructor.Estado;

                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //Modelo.Instructor instructor = new Modelo.Instructor()
            //{
            //    TipoDocumento = ddlTipoDocumento.SelectedValue,
            //    NumeroDocumento = txtNumeroDocumento.Text,
            //    Nombre = txtNombre.Text,
            //    Apellido = txtApellido.Text,
            //    Correo = txtCorreo.Text,
            //    Telefono = txtTelefono.Text,
            //    Contrasena = txtContrasena.Text,
            //    Estado = ddlEstado.SelectedValue
            //};

            //bool resultado = oInstructor.MtRegistrarInstructor(instructor);

            //if (resultado)
            //{
            //    MtCargarInstructores();
            //    MtLimpiarFormulario();

            //    ClientScript.RegisterStartupScript(
            //        this.GetType(),
            //        "mensaje",
            //        "alert('Instructor registrado correctamente');",
            //        true);
            //}


        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Modelo.Instructor instructor = new Modelo.Instructor()
            {
                Id = Convert.ToInt32(hfIdInstructor.Value),
                TipoDocumento = ddlTipoDocumento.SelectedValue,
                NumeroDocumento = txtNumeroDocumento.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Correo = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Contrasena = txtContrasena.Text,
                Estado = ddlEstado.SelectedValue
            };

            bool resultado = oInstructor.MtActualizarInstructor(instructor);

            if (resultado)
            {
                MtCargarInstructores();
                MtLimpiarFormulario();

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "mensaje",
                    "alert('Instructor actualizado correctamente');",
                    true);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            MtLimpiarFormulario();
        }

        protected void btnAsignarFicha_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int idInstructor = Convert.ToInt32(btn.CommandArgument);

            Modelo.Instructor instructor =
                oInstructor.MtObtenerInstructorPorId(idInstructor);

            if (instructor != null)
            {
                hfIdInstructor.Value = instructor.Id.ToString();

                lblInstructor.Text =
                    instructor.Nombre + " " + instructor.Apellido;

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

            Modelo.FichaInstructor asignacion = new Modelo.FichaInstructor()
            {
                Ficha = new Modelo.Ficha()
                {
                    Id = Convert.ToInt32(
                    ddlFichaAsignar.SelectedValue)
                },

                Instructor = new Modelo.Instructor()
                {
                    Id = Convert.ToInt32(
                    hfIdInstructor.Value)
                }
            };

            bool resultado =
                oFichaInstructor.MtRegistrarFichaInstructor(asignacion);

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

            int idInstructor =
                Convert.ToInt32(btn.CommandArgument);

            Modelo.Instructor instructor =
                oInstructor.MtObtenerInstructorPorId(idInstructor);

            lblNombreInstructor.Text =
                instructor.Nombre + " " + instructor.Apellido;

            gvFichasInstructor.DataSource =
                oFichaInstructor.MtListarFichaPorInstructor(idInstructor);

            gvFichasInstructor.DataBind();

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
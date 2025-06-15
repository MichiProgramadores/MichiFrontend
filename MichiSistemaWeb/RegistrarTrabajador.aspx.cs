using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MichiSistemaWeb.MichiBackend;

namespace MichiSistemaWeb
{
    public partial class RegistrarTrabajador : System.Web.UI.Page
    {

        protected TrabajadorWSClient trabajadorService;
        protected trabajador trabajador;
        protected Estado estado;
        protected void Page_Init(object sender, EventArgs e)
        {
            trabajadorService = new TrabajadorWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Obtener los valores del enum tipoCliente
                ddlTipoTrabajador.DataSource = Enum.GetValues(typeof(tipoTrabajador));
                ddlTipoTrabajador.DataBind();

                // Opcional: agregar un valor por defecto
                ddlTipoTrabajador.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }

            string accion = Request.QueryString["accion"];
            if (accion == null)
            {

                estado = Estado.Nuevo;
                trabajador = new trabajador();
                lblTitulo.Text = "Registrar Trabajador";
                lblID.Visible = false;
                txtIDTrabajador.Visible = false;
                lblActivo.Visible = false;
                txtActivo.Visible = false;

            }
            else if (accion == "modificar")
            {
                estado = Estado.Modificar;
                lblTitulo.Text = "Modificar Trabajador";
                trabajador = (trabajador)Session["trabajadorSeleccionado"];
                if (!IsPostBack)
                {
                    AsignarValoresTexto();
                }
                 lblID.Visible = true;
                 txtIDTrabajador.Visible = true;
                 txtIDTrabajador.Enabled = false;
                lblActivo.Visible = false;
                txtActivo.Visible = false;
            }
            else if (accion == "ver")
            {
                lblTitulo.Text = "Ver Trabajador";
                trabajador = (trabajador)Session["trabajadorSeleccionado"];
                AsignarValoresTexto();
                txtIDTrabajador.Enabled = false;
                txtNombres.Enabled = false;
                txtApellidos.Enabled = false;
                txtCelular.Enabled = false;
                txtEmail.Enabled = false;
                ddlTipoTrabajador.Enabled = false;
                btnGuardar.Visible = false;
                lblActivo.Visible = true;
                txtActivo.Visible = true;
                txtActivo.Enabled = false;
            }

        }

        protected void AsignarValoresTexto()
        {


            txtIDTrabajador.Text = trabajador.persona_id.ToString();
            txtNombres.Text = trabajador.nombres;
            txtApellidos.Text = trabajador.apellidos;
            txtCelular.Text = trabajador.celular.ToString();
            txtEmail.Text = trabajador.email;
            ddlTipoTrabajador.SelectedValue = trabajador.tipoTrabajador.ToString();
        }
        private void AsignarValoresTrabajador()
        {
            
            trabajador.nombres = txtNombres.Text.Trim();
            trabajador.apellidos = txtApellidos.Text.Trim();
            trabajador.celular = int.Parse(txtCelular.Text.Trim());
            trabajador.email = txtEmail.Text.Trim();

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación básica
                if (string.IsNullOrWhiteSpace(txtNombres.Text) ||
                    string.IsNullOrWhiteSpace(txtApellidos.Text) ||
                    string.IsNullOrWhiteSpace(txtCelular.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(ddlTipoTrabajador.SelectedValue))
                {
                    lanzarMensajedeError("Por favor, complete todos los campos.");
                    return;
                }

                // Asignar los valores del formulario al objeto cliente
                AsignarValoresTrabajador();
                // Insertar trabajador
                string valorSeleccionado = ddlTipoTrabajador.SelectedValue;
               

                if (estado == Estado.Nuevo)
                {
                    trabajadorService.registrarTrabajador(trabajador, valorSeleccionado);
                }
                else if (estado == Estado.Modificar)
                {
                    trabajadorService.actualizarTrabajador(trabajador);
                }

                // Redirigir
                Response.Redirect("ListarTrabajadores.aspx");
            }
            catch (Exception ex)
            {
                lanzarMensajedeError("No se pudo registrar el trabajador: " + ex.Message);
            }
        }



        public void lanzarMensajedeError(String mensaje)
        {
            lblMensajeError.Text = mensaje;
            string script = "mostrarModalError();";
            ScriptManager.RegisterStartupScript(this, GetType(), "modalError", script, true);
        }

        public void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarTrabajadores.aspx");
        }
    }
}

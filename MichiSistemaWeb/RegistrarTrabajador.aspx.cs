using System;
using System.Collections.Generic;
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
                ddlTipoTrabajador.Items.Insert(0, new ListItem("Seleccione...", ""));
            }

        }

        protected void AsignarValores()
        {
            trabajador = new trabajador();
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
                AsignarValores();

                // Insertar cliente
                string valorSeleccionado = ddlTipoTrabajador.SelectedValue;
                trabajadorService.registrarTrabajador(trabajador, valorSeleccionado);

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
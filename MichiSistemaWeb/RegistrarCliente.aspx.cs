using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MichiSistemaWeb.MichiBackend;

namespace MichiSistemaWeb
{
    public partial class RegistrarCliente : System.Web.UI.Page
    {

        protected ClienteWSClient clienteService;
        protected cliente cliente;

        protected void Page_Init(object sender, EventArgs e)
        {
            clienteService = new ClienteWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Obtener los valores del enum tipoCliente
                ddlTipoCliente.DataSource = Enum.GetValues(typeof(tipoCliente));
                ddlTipoCliente.DataBind();

                // Opcional: agregar un valor por defecto
                ddlTipoCliente.Items.Insert(0, new ListItem("Seleccione...", ""));
            }

        }

        protected void AsignarValores()
        {
            cliente = new cliente();
            cliente.nombres = txtNombres.Text.Trim();
            cliente.apellidos = txtApellidos.Text.Trim();
            cliente.celular = int.Parse(txtCelular.Text.Trim());
            cliente.email = txtEmail.Text.Trim();

            /*
            string valorSeleccionado = ddlTipoCliente.SelectedValue;
            lblMensajeError.Text = "Tipo seleccionado: " + valorSeleccionado; // DEBUG
            System.Diagnostics.Debug.WriteLine("Tipo seleccionado: " + ddlTipoCliente.SelectedValue);

            if (string.IsNullOrEmpty(valorSeleccionado))
            {
                throw new Exception("Debe seleccionar un tipo de cliente.");
            }

            cliente.tipoCliente = (tipoCliente)Enum.Parse(typeof(tipoCliente), valorSeleccionado);
            */
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
                    string.IsNullOrWhiteSpace(ddlTipoCliente.SelectedValue))
                {
                    lanzarMensajedeError("Por favor, complete todos los campos.");
                    return;
                }

                // Asignar los valores del formulario al objeto cliente
                AsignarValores();

                // Insertar cliente
                string valorSeleccionado = ddlTipoCliente.SelectedValue;
                clienteService.registrarCliente(cliente, valorSeleccionado);

                // Redirigir
                Response.Redirect("ListarClientes.aspx");
            }
            catch (Exception ex)
            {
                lanzarMensajedeError("No se pudo registrar el cliente: " + ex.Message);
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
            Response.Redirect("ListarClientes.aspx");
        }
    }
}
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
        protected Estado estado;

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
                ddlTipoCliente.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }

            string accion = Request.QueryString["accion"];
            if (accion == null)
            {
                estado = Estado.Nuevo;
                cliente = new cliente();
                lblTitulo.Text = "Registrar cliente";

                lblID.Visible = false;
                txtIDCliente.Visible = false;

                lblPuntuacion.Visible = false;
                txtPuntuacion.Visible = false;

                lblActivo.Visible = false;
                txtActivo.Visible = false;

            }
            else if (accion == "modificar")
            {
                estado = Estado.Modificar;
                lblTitulo.Text = "Modificar cliente";
                cliente = (cliente)Session["clienteSeleccionado"];
                if (!IsPostBack)
                {
                    AsignarValoresTexto();
                }
                lblID.Visible = true;
                txtIDCliente.Visible = true;
                txtIDCliente.Enabled = false;

                lblPuntuacion.Visible = true;
                txtPuntuacion.Visible = true;
                txtPuntuacion.Enabled = false;

                lblActivo.Visible = false;
                txtActivo.Visible = false;
            }
            else if (accion == "ver")
            {
                lblTitulo.Text = "Ver cliente";
                cliente = (cliente)Session["clienteSeleccionado"];
                AsignarValoresTexto();

                lblID.Visible = true;
                txtIDCliente.Visible = true;
                txtIDCliente.Enabled = false;

                lblPuntuacion.Visible = true;
                txtPuntuacion.Visible = true;
                txtPuntuacion.Enabled = false;

                lblActivo.Visible = true;
                txtActivo.Visible = true;
                txtActivo.Enabled = false;

                txtNombres.Enabled = false;
                txtApellidos.Enabled = false;
                txtCelular.Enabled = false;
                txtEmail.Enabled = false;
                txtNumeroTipoCliente.Enabled = false;

                ddlTipoCliente.Enabled = false;

                btnGuardar.Visible = false;
            }

        }

        protected void AsignarValoresTexto()
        {
            txtIDCliente.Text = cliente.persona_id.ToString();
            txtNombres.Text = cliente.nombres;
            txtApellidos.Text = cliente.apellidos;
            txtCelular.Text = cliente.celular.ToString();
            txtEmail.Text = cliente.email;
            txtNumeroTipoCliente.Text = cliente.numeroTipoCliente;
            txtPuntuacion.Text = cliente.puntuacion.ToString();

            txtActivo.Text = cliente.estado?"Activo":"Inactivo";

            ddlTipoCliente.SelectedValue = cliente.tipoCliente.ToString();
        }

        protected void AsignarValoresCliente()
        {
            cliente.nombres = txtNombres.Text.Trim();
            cliente.apellidos = txtApellidos.Text.Trim();
            cliente.celular = int.Parse(txtCelular.Text.Trim());
            cliente.email = txtEmail.Text.Trim();
            cliente.numeroTipoCliente = txtNumeroTipoCliente.Text.Trim();
            cliente.puntuacion = 100;
            /* DEBUG 1:
            string valorSeleccionado = ddlTipoCliente.SelectedValue;
            lblMensajeError.Text = "Tipo seleccionado: " + valorSeleccionado; // DEBUG
            System.Diagnostics.Debug.WriteLine("Tipo seleccionado: " + ddlTipoCliente.SelectedValue);

            if (string.IsNullOrEmpty(valorSeleccionado))
            {
                throw new Exception("Debe seleccionar un tipo de cliente.");
            }
            */

            /* DEBUG 2:
            string valorSeleccionado = ddlTipoCliente.SelectedValue;
            //cliente.tipoCliente = (tipoCliente)Enum.Parse(typeof(tipoCliente), valorSeleccionado);
            //cliente.tipoCliente = tipoCliente.EIN;

            System.Diagnostics.Debug.WriteLine("Tipo seleccionado: " + cliente.tipoCliente.ToString());
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
                AsignarValoresCliente();

                // Insertar o modificar el cliente:
                string valorSeleccionado = ddlTipoCliente.SelectedValue;
                

                if (estado == Estado.Nuevo)
                {
                    clienteService.registrarCliente(cliente, valorSeleccionado);
                }
                else if (estado == Estado.Modificar)
                {
                    clienteService.actualizarCliente(cliente, valorSeleccionado);
                }

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
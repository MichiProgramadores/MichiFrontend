using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class RegistrarUsuario : System.Web.UI.Page
    {
        protected UsuarioWSClient usuarioService;
        protected usuario usuario;
        protected Estado estado;
        protected void Page_Init(object sender, EventArgs e)
        {
            usuarioService = new UsuarioWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                
            }
            string accion = Request.QueryString["accion"];
            if (accion == null)
            {
                estado = Estado.Nuevo;
                usuario = new usuario();
                lblTitulo.Text = "Registrar Usuario";
                lblUser.Visible = false;
                txtNombre.Visible = false;
                txtNombre.Enabled = false;

            }
            else if (accion == "modificar")
            {
                estado = Estado.Modificar;
                lblTitulo.Text = "Modificar Usuario";
                usuario = (usuario)Session["usuarioSeleccionado"];
                if (!IsPostBack)
                {
                    AsignarValoresTexto();
                }
                lblID.Visible = true;
                txtIDTrabajador.Visible = true;
                txtIDTrabajador.Enabled = false;
                lblContra.Visible = true;
                txtContra.Visible = true;
                txtContra.Enabled = true;

                lblUser.Visible = true;
                txtNombre.Visible = true;
                txtNombre.Enabled = true;
            }

        }
        protected void AsignarValoresTexto()
        {
            txtIDTrabajador.Text = usuario.id.ToString();
            txtContra.Text = usuario.contrasena;
            txtNombre.Text = usuario.nombreUsuario;
        }
        private void AsignarValoresTrabajador()
        {

            usuario.id = int.Parse(txtIDTrabajador.Text.Trim());
            usuario.nombreUsuario= txtNombre.Text.Trim();
            usuario.contrasena = txtContra.Text.Trim();

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación básica
                if (string.IsNullOrWhiteSpace(txtIDTrabajador.Text) ||
                    string.IsNullOrWhiteSpace(txtContra.Text))
                   // string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    lanzarMensajedeError("Por favor, complete todos los campos.");
                    return;
                }
                AsignarValoresTrabajador();

                if (estado == Estado.Nuevo)
                {
                    //if (usuarioService.obtenerUsuario(usuario.id) != null)
                    //{
                    //   lanzarMensajedeError("Este trabajador ya tiene un usuario.");
                        
                    //}
                    usuarioService.registrarUsuario(usuario);
                }
                else if (estado == Estado.Modificar)
                {

                    usuarioService.actualizarUsuario(usuario);
                }

                // Redirigir
                Response.Redirect("ListarUsuarios.aspx");
            }
            catch (Exception ex)
            {
                lanzarMensajedeError("No se pudo registrar el usuario: " + ex.Message);
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
            Response.Redirect("ListarUsuarios.aspx");
        }
    }
}
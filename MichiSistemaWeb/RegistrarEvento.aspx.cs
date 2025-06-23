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
    public partial class RegistrarEvento : System.Web.UI.Page
    {
        protected EventoWSClient eventoService;
        protected evento evento;
        protected Estado estado;

        protected void Page_Init(object sender, EventArgs e)
        {
            eventoService = new EventoWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlTipoEvento.DataSource = Enum.GetValues(typeof(tipoEvento));
                ddlTipoEvento.DataBind();
                ddlTipoEvento.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }

            string accion = Request.QueryString["accion"];

            if (accion == null)
            {
                estado = Estado.Nuevo;
                evento = new evento();
                lblTitulo.Text = "Registrar evento";
                lblID.Visible = false;
                txtIDEvento.Visible = false;
            }
            else if (accion == "modificar")
            {
                estado = Estado.Modificar;
                lblTitulo.Text = "Modificar evento";
                evento = (evento)Session["eventoSeleccionado"];
                if (!IsPostBack)
                {
                    AsignarValoresTexto();
                }

                lblID.Visible = true;
                txtIDEvento.Visible = true;
                txtIDEvento.Enabled = false;
            }
            else if (accion == "ver")
            {
                lblTitulo.Text = "Ver evento";
                evento = (evento)Session["eventoSeleccionado"];
                AsignarValoresTexto();
                
                txtIDEvento.Enabled = false;
                txtFechaInicio.Enabled = false;
                txtFechaFin.Enabled = false;
                txtHoraInicio.Enabled = false;
                txtHoraFin.Enabled = false;
                txtDireccion.Enabled = false;
                txtCodigoPostal.Enabled = false;
                txtDescripcion.Enabled = false;
                ddlTipoEvento.Enabled = false;
                btnGuardar.Visible = false;
            }
        }

        protected void AsignarValoresTexto()
        {
            txtIDEvento.Text = evento.evento_id.ToString();
            txtFechaInicio.Text = evento.fechaInicio.ToString("yyyy-MM-dd");
            txtFechaFin.Text = evento.fechaFin.ToString("yyyy-MM-dd");
            //txtHoraInicio.Text = evento.horaInicio.ToString(@"hh\:mm");
            //txtHoraFin.Text = evento.horaFin.ToString(@"hh\:mm");
            txtDireccion.Text = evento.direccion;
            txtCodigoPostal.Text = evento.codigoPostal;
            txtDescripcion.Text = evento.descripcion;
            ddlTipoEvento.SelectedValue = evento.tipoEvento.ToString();
        }

        protected void AsignarValoresEvento()
        {
            evento.fechaInicio = DateTime.Parse(txtFechaInicio.Text);
            evento.fechaFin = DateTime.Parse(txtFechaFin.Text);
            //evento.horaInicio = TimeSpan.Parse(txtHoraInicio.Text);
            //evento.horaFin = TimeSpan.Parse(txtHoraFin.Text);
            evento.direccion = txtDireccion.Text.Trim();
            evento.codigoPostal = txtCodigoPostal.Text.Trim();
            evento.descripcion = txtDescripcion.Text.Trim();
            evento.tipoEvento = (tipoEvento)Enum.Parse(typeof(tipoEvento), ddlTipoEvento.SelectedValue);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFechaInicio.Text) ||
                    string.IsNullOrWhiteSpace(txtFechaFin.Text) ||
                    //string.IsNullOrWhiteSpace(txtHoraInicio.Text) ||
                    //string.IsNullOrWhiteSpace(txtHoraFin.Text) ||
                    string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                    string.IsNullOrWhiteSpace(txtCodigoPostal.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                    string.IsNullOrWhiteSpace(ddlTipoEvento.SelectedValue))
                {
                    lanzarMensajedeError("Por favor, complete todos los campos.");
                    return;
                }

                AsignarValoresEvento();

                if (estado == Estado.Nuevo)
                {
                    //eventoService.registrarEvento(evento, ddlTipoEvento.SelectedValue);
                    eventoService.registrarEvento(evento);
                }
                else if (estado == Estado.Modificar)
                {
                    eventoService.actualizarEvento(evento);
                }

                Response.Redirect("ListarEventos.aspx");
            }
            catch (Exception ex)
            {
                lanzarMensajedeError("No se pudo registrar el evento: " + ex.Message);
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
            Response.Redirect("ListarEventos.aspx");
        }
    }
}

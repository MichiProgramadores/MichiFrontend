using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class ListarUsuarios : System.Web.UI.Page
    {
        protected UsuarioWSClient usuarioWS;
        protected List<usuario> usuarios;
        protected TrabajadorWSClient trabajadorWS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["trabajador"] != null)
            {
                trabajador trabajador = (trabajador)Session["trabajador"];

                // Verificas si es admin
                if (trabajador.nombres != "Oveja")
                {
                    lbRegistrar.Visible = false;
                }
            }
            else
            {
                lbRegistrar.Visible = false;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            usuarioWS = new UsuarioWSClient();
            trabajadorWS = new TrabajadorWSClient();
            CargarDatos();
        }
        protected void CargarDatos()
        {
            var usuarios = usuarioWS.listarUsuarios();

            // Verifica si la lista no es nula y si tiene elementos
            if (usuarios != null && usuarios.Length > 0)
            {
                dgvUsuarios.DataSource = usuarios;
            }
            else
            {
                // Si la lista está vacía o nula, puedes asignar una lista vacía
                dgvUsuarios.DataSource = new List<usuario>(); // Asignando una lista vacía
            }

            dgvUsuarios.DataBind();
        }

        protected void lbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto del textbox
                string textoId = txtNombreID.Text.Trim();
                if (int.TryParse(textoId, out int idUsuario))
                {
                    // Buscar cliente por ID usando tu capa de negocio o servicio
                    var usuario = usuarioWS.obtenerUsuario(idUsuario);
                    if (usuario != null)
                    {
                        // Si encontró el cliente, lo pone en una lista para enlazar
                        var lista = new List<usuario> { usuario };
                        dgvUsuarios.DataSource = lista;
                        dgvUsuarios.DataBind();
                        //lblMensaje.Text = "";
                    }
                    else
                    {
                        // Si no encontró resultados
                        dgvUsuarios.DataSource = null;
                        dgvUsuarios.DataBind();
                        // lblMensaje.Text = "No se encontró cliente con ese ID.";
                    }
                }
                else
                {
                    // Si no ingresó un número válido
                    dgvUsuarios.DataSource = usuarios; // Volver a mostrar todos los productos
                    dgvUsuarios.DataBind();
                    //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                }
            }
            catch (Exception ex)
            {
                // lblMensaje.Text = "Error al buscar cliente: " + ex.Message;
            }
            //txtNombreID.Text = "";
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarUsuario.aspx");
        }
        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            usuario usuario = usuarios.SingleOrDefault(x => x.id == idEmpleado);
            Session["usuarioSeleccionado"] = usuario;
            Response.Redirect("RegistrarUsuario.aspx?accion=modificar");
        }
        protected void dgvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsuarios.PageIndex = e.NewPageIndex;
            dgvUsuarios.DataBind();
        }
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idUsuario = int.Parse(hfIdEliminar.Value);
            trabajador trabajador = (trabajador)Session["trabajador"];
            int idSesion = trabajador.persona_id;

            if (idUsuario == idSesion)
            {
                lanzarMensajedeError("ERROR: No se puede eliminar a si mismo");
            }
            else
            {
                usuarioWS.eliminarUsuario(idUsuario);
                Response.Redirect("ListarUsuarios.aspx");
            }
        }

        public void lanzarMensajedeError(String mensaje)
        {
            lblMensajeError.Text = mensaje;
            string script = "mostrarModalError();";
            ScriptManager.RegisterStartupScript(this, GetType(), "modalError", script, true);
        }

        protected void dgvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = DataBinder.Eval(e.Row.DataItem, "id").ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "nombreUsuario").ToString();

                var idValue = DataBinder.Eval(e.Row.DataItem, "id");

                if (idValue != null && int.TryParse(idValue.ToString(), out int idTrabajador))
                {

                    // Buscar cliente por ID usando tu capa de negocio o servicio

                    var trabajador = trabajadorWS.obtenerTrabajador(idTrabajador);

                    if (trabajador != null)
                    {
                        // Si encontró el cliente, lo pone en una lista para enlaza
                        e.Row.Cells[2].Text = trabajador.nombres;
                        e.Row.Cells[3].Text = trabajador.apellidos;
                    }
                    else
                    {
                        e.Row.Cells[2].Text = "No encontrado";
                        e.Row.Cells[3].Text = "No encontrado";
                    }
                }

                // Obtén los controles dentro de la fila
                LinkButton lbEliminar = (LinkButton)e.Row.FindControl("lbEliminar");
                LinkButton lbModificar = (LinkButton)e.Row.FindControl("lbModificar");

                // Verificar el rol del usuario (suponiendo que tienes un rol almacenado en la sesión)
                if (Session["trabajador"] != null)
                {
                    trabajador trabajador = (trabajador)Session["trabajador"];

                    // Verificas si es admin
                    if (trabajador.nombres == "Oveja")
                    {
                        lbEliminar.Visible = true;
                        lbModificar.Visible = true;
                    }
                    else
                    {
                        lbEliminar.Visible = false;
                        lbModificar.Visible = false;

                    }
                }
                else
                {
                    lbEliminar.Visible = false;
                    lbModificar.Visible = false;
                }
            }
        }

    }
}
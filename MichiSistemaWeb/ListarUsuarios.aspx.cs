using MichiSistemaWeb.MichiBackend;
using System;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["trabajador"] != null)
            {
                trabajador trabajador = (trabajador)Session["trabajador"];

                // Verificas si es admin
                if (trabajador.nombres != "Carlos")
                {
                    lbRegistrar.Visible = false;
                   
                    // También puedes recorrer la grilla para ocultar botones
                    //dgvUsuarios.Columns[dgvUsuarios.Columns.Count - 1].Visible = false;
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
            CargarDatos();
        }
        protected void CargarDatos()
        {
            usuarios = usuarioWS.listarUsuarios().ToList();
            dgvUsuarios.DataSource = usuarios;
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
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

        }
        protected void lbModificar_Click(object sender, EventArgs e)
        {
     

        }


        protected void dgvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsuarios.PageIndex = e.NewPageIndex;
            dgvUsuarios.DataBind();
        }

        protected void dgvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = DataBinder.Eval(e.Row.DataItem, "id").ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "nombreUsuario").ToString();
              
               
            }
        }
    }
}
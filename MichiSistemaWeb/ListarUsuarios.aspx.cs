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
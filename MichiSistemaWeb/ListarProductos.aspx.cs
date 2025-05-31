using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class ListarProductos : System.Web.UI.Page
    {
        //private ProductoBO boProducto;
        //private BindingList<Producto> productos;
        protected void Page_Load(object sender, EventArgs e)
        {
            //boProducto = new ProductoBO();
            //productos = boProducto.listarTodos();
            //dgvProducto.DataSource = productos;
            dgvProductos.DataBind();
        }

        protected void dgvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "Nombre").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "Estado").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "Tipo").ToString();
                e.Row.Cells[4].Text = DataBinder.Eval(e.Row.DataItem, "Precio").ToString();
                e.Row.Cells[5].Text = DataBinder.Eval(e.Row.DataItem, "Stock minimo").ToString();
                e.Row.Cells[6].Text = DataBinder.Eval(e.Row.DataItem, "Stock actual").ToString();
                e.Row.Cells[7].Text = DataBinder.Eval(e.Row.DataItem, "Unidad de medida").ToString();
                e.Row.Cells[8].Text = DataBinder.Eval(e.Row.DataItem, "Descripción").ToString();
            }
        }

        protected void dgvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvProductos.PageIndex = e.NewPageIndex;
            dgvProductos.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarProducto.aspx");
        }

        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument); //queda actualizar esto por producto
            //Empleado empleado = empleados.SingleOrDefault(x => x.IdPersona == idEmpleado);
            // Session["empleadoSeleccionado"] = empleado;
            Response.Redirect("RegistrarProducto.aspx?accion=modificar");
        }
        
        protected void lbEliminar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            //boEmpleado.eliminar(idEmpleado);
            Response.Redirect("ListarProductos.aspx");
        }

        protected void lbVisualizar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            //Empleado empleado = empleados.SingleOrDefault(x => x.IdPersona == idEmpleado);
            //Session["empleadoSeleccionado"] = empleado;
            Response.Redirect("RegistrarProducto.aspx?accion=ver");
        }

        protected void lbBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
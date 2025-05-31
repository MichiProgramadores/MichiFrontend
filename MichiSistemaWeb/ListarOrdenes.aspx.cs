using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class ListarOrdenes : System.Web.UI.Page
    {
        //private ProductoBO boProducto;
        //private BindingList<Producto> productos;
        protected void Page_Load(object sender, EventArgs e)
        {
            //boProducto = new ProductoBO();
            //productos = boProducto.listarTodos();
            //dgvProducto.DataSource = productos;
            //dgvOrdenes.DataBind();
        }

        protected void dgvOrdenes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "Tipo recepción").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "Set up personalizado").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "Monto total").ToString();
                e.Row.Cells[4].Text = DataBinder.Eval(e.Row.DataItem, "Saldo").ToString();
                e.Row.Cells[5].Text = DataBinder.Eval(e.Row.DataItem, "Cantidad dias").ToString();
                e.Row.Cells[6].Text = DataBinder.Eval(e.Row.DataItem, "Fecha devolución").ToString();
                e.Row.Cells[7].Text = DataBinder.Eval(e.Row.DataItem, "Fecha emisión").ToString();
                e.Row.Cells[8].Text = DataBinder.Eval(e.Row.DataItem, "Fecha registro").ToString();
                e.Row.Cells[9].Text = DataBinder.Eval(e.Row.DataItem, "Fecha entrega").ToString();
                e.Row.Cells[10].Text = DataBinder.Eval(e.Row.DataItem, "Tipo estado devolución").ToString();
                e.Row.Cells[11].Text = DataBinder.Eval(e.Row.DataItem, "Id cliente").ToString();
                e.Row.Cells[12].Text = DataBinder.Eval(e.Row.DataItem, "Id trabajador").ToString();
            }
        }

        protected void dgvOrdenes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //dgvOrdenes.PageIndex = e.NewPageIndex;
            //dgvOrdenes.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarOrden.aspx");
        }

        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument); //queda actualizar esto por ordenes
            //Empleado empleado = empleados.SingleOrDefault(x => x.IdPersona == idEmpleado);
            // Session["empleadoSeleccionado"] = empleado;
            Response.Redirect("RegistrarOrden.aspx?accion=modificar");
        }

        protected void lbEliminar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            //boEmpleado.eliminar(idEmpleado);
            Response.Redirect("ListarOrdenes.aspx");
        }

        protected void lbVisualizar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            //Empleado empleado = empleados.SingleOrDefault(x => x.IdPersona == idEmpleado);
            //Session["empleadoSeleccionado"] = empleado;
            Response.Redirect("RegistrarOrden.aspx?accion=ver");
        }

        protected void lbBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
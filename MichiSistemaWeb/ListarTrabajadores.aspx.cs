using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class ListarTrabajadores : System.Web.UI.Page
    {
        
        //private EmpleadoBO boEmpleado;
        //private BindingList<Empleado> empleados;
        protected void Page_Load(object sender, EventArgs e)
        {
            //boEmpleado = new EmpleadoBO();
            //empleados = boEmpleado.listarTodos();
            //dgvEmpleados.DataSource = empleados;
            dgvEmpleados.DataBind();
        }

        protected void dgvEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = DataBinder.Eval(e.Row.DataItem, "DNI").ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "Nombre").ToString() + " " + DataBinder.Eval(e.Row.DataItem, "ApellidoPaterno").ToString();
               // e.Row.Cells[2].Text = ((Area)DataBinder.Eval(e.Row.DataItem, "Area")).Nombre;
            }
        }

        protected void dgvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEmpleados.PageIndex = e.NewPageIndex;
            dgvEmpleados.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarTrabajador.aspx");
        }

        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            //Empleado empleado = empleados.SingleOrDefault(x => x.IdPersona == idEmpleado);
           // Session["empleadoSeleccionado"] = empleado;
            Response.Redirect("RegistrarTrabajador.aspx?accion=modificar");
        }

        protected void lbEliminar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            //boEmpleado.eliminar(idEmpleado);
            Response.Redirect("ListarEmpleados.aspx");
        }

        protected void lbVisualizar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            //Empleado empleado = empleados.SingleOrDefault(x => x.IdPersona == idEmpleado);
            //Session["empleadoSeleccionado"] = empleado;
            Response.Redirect("RegistrarEmpleado.aspx?accion=ver");
        }

        protected void lbBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
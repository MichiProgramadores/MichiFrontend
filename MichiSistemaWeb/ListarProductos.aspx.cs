using MichiSistemaWeb.MichiBackend;
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
        protected ProductoWSClient productoWS;
        protected void Page_Load(object sender, EventArgs e)
        {
            productoWS = new ProductoWSClient();
            CargarDatos();
        }
        protected void CargarDatos()
        {
            dgvProductos.DataSource = productoWS.listarProductos();
            dgvProductos.DataBind();
        }

        protected void dgvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = DataBinder.Eval(e.Row.DataItem, "producto_id").ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "nombre").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "estado").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "categoriaProducto").ToString();
                e.Row.Cells[4].Text = DataBinder.Eval(e.Row.DataItem, "precio").ToString();
                e.Row.Cells[5].Text = DataBinder.Eval(e.Row.DataItem, "stockMinimo").ToString();
                e.Row.Cells[6].Text = DataBinder.Eval(e.Row.DataItem, "stockActual").ToString();
                e.Row.Cells[7].Text = DataBinder.Eval(e.Row.DataItem, "unidadMedida").ToString();
                e.Row.Cells[8].Text = DataBinder.Eval(e.Row.DataItem, "descripcion").ToString();
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
            try
            {
                // Obtener el texto del textbox
                string textoId = txtID.Text.Trim();

                if (int.TryParse(textoId, out int idProducto))
                {
                    // Buscar cliente por ID usando tu capa de negocio o servicio

                    var producto = productoWS.obtenerProducto(idProducto);

                    if (producto != null)
                    {
                        // Si encontró el cliente, lo pone en una lista para enlazar
                        var lista = new List<producto> { producto };
                        dgvProductos.DataSource = lista;
                        dgvProductos.DataBind();
                        //lblMensaje.Text = "";
                    }
                    else
                    {
                        // Si no encontró resultados
                        dgvProductos.DataSource = null;
                        dgvProductos.DataBind();
                        // lblMensaje.Text = "No se encontró cliente con ese ID.";
                    }
                }
                else
                {
                    // Si no ingresó un número válido
                    dgvProductos.DataSource = null;
                    dgvProductos.DataBind();
                    //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                }
            }
            catch (Exception ex)
            {
                // lblMensaje.Text = "Error al buscar cliente: " + ex.Message;
            }

        }
    }
}
using MichiSistemaWeb.MichiBackend;
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
		
        protected OrdenWSClient ordenWS;
        protected void Page_Load(object sender, EventArgs e)
        {
            ordenWS = new OrdenWSClient();
            CargarDatos();
        }
        protected void CargarDatos()
        {
            var lista= ordenWS.listarOrdenes().ToList();
            dgvOrdenes.DataSource = lista;
            dgvOrdenes.DataBind();
        }


        private string FormatDateTime(object obj, string format = "dd/MM/yyyy HH:mm:ss")
        {
            if (obj == null || obj == DBNull.Value) return "";
            string str = obj.ToString();
            if (DateTime.TryParse(str, out DateTime dt))
                return dt.ToString(format);
            return str; // O "" si prefieres ocultar valores no parseables
        }

        protected void dgvOrdenes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = DataBinder.Eval(e.Row.DataItem, "idOrden").ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "tipoRecepcion").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "setUpPersonalizado").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "totalPagar").ToString();
                e.Row.Cells[4].Text = DataBinder.Eval(e.Row.DataItem, "saldo").ToString();
                e.Row.Cells[5].Text = DataBinder.Eval(e.Row.DataItem, "cantDias").ToString();

                e.Row.Cells[6].Text = FormatDateTime(DataBinder.Eval(e.Row.DataItem, "fecha_devolucion"), "dd/MM/yyyy");
                e.Row.Cells[7].Text = FormatDateTime(DataBinder.Eval(e.Row.DataItem, "fecha_emisión"), "dd/MM/yyyy");
                e.Row.Cells[8].Text = FormatDateTime(DataBinder.Eval(e.Row.DataItem, "fecha_registro"), "dd/MM/yyyy HH:mm:ss");
                e.Row.Cells[9].Text = FormatDateTime(DataBinder.Eval(e.Row.DataItem, "fecha_entrega"), "dd/MM/yyyy");

                e.Row.Cells[10].Text = DataBinder.Eval(e.Row.DataItem, "tipoEstadoDevolucion").ToString();
                e.Row.Cells[11].Text = DataBinder.Eval(e.Row.DataItem, "clienteID").ToString();
                e.Row.Cells[12].Text = DataBinder.Eval(e.Row.DataItem, "trabajadorID").ToString();
            }
        }


        protected void dgvOrdenes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvOrdenes.PageIndex = e.NewPageIndex;
            dgvOrdenes.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarOrden.aspx");
        }

        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            //Empleado empleado = empleados.SingleOrDefault(x => x.IdPersona == idEmpleado);
            // Session["empleadoSeleccionado"] = empleado;
            Response.Redirect("RegistrarOrden.aspx?accion=modificar");
        }

        protected void lbEliminar_Click(object sender, EventArgs e)
        {
            int idOrden = Int32.Parse(((LinkButton)sender).CommandArgument);
            ordenWS.eliminarOrden(idOrden);
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
            try
            {
                // Obtener el texto del textbox
                string textoId = txtNombre.Text.Trim();

                if (int.TryParse(textoId, out int idOrden))
                {
                    // Buscar cliente por ID usando tu capa de negocio o servicio
                    var orden = ordenWS.obtenerOrden(idOrden);

                    if (orden != null)
                    {
                        // Si encontró el cliente, lo pone en una lista para enlazar
                        var lista = new List<orden> { orden };
                        dgvOrdenes.DataSource = lista;
                        dgvOrdenes.DataBind();
                        //lblMensaje.Text = "";
                    }
                    else
                    {
                        // Si no encontró resultados
                        dgvOrdenes.DataSource = null;
                        dgvOrdenes.DataBind();
                        // lblMensaje.Text = "No se encontró cliente con ese ID.";
                    }
                }
                else
                {
                    // Si no ingresó un número válido
                    dgvOrdenes.DataSource = null;
                    dgvOrdenes.DataBind();
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
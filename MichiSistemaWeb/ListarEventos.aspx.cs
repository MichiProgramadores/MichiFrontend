using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class ListarEventos : System.Web.UI.Page
    {
        protected EventoWSClient eventoWS;
        protected void Page_Load(object sender, EventArgs e)
        {
            eventoWS = new EventoWSClient();
            CargarDatos();
        }
        protected void CargarDatos()
        {
            dgvEventos.DataSource = eventoWS.listarEventos();
            dgvEventos.DataBind();
        }

        private string FormatDate(object obj, string format = "dd/MM/yyyy")
        {
            if (obj == null || obj == DBNull.Value) return "";
            if (DateTime.TryParse(obj.ToString(), out DateTime dt))
                return dt.ToString(format);
            return obj.ToString();
        }

        private string FormatTime(object obj, string format = "HH:mm")
        {
            if (obj == null || obj == DBNull.Value) return "";
            if (DateTime.TryParse(obj.ToString(), out DateTime dt))
                return dt.ToString(format);
            return obj.ToString();
        }

        protected void dgvEventos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[1].Text = FormatDate(DataBinder.Eval(e.Row.DataItem, "fechaInicio"));
                //e.Row.Cells[2].Text = FormatDate(DataBinder.Eval(e.Row.DataItem, "fechaFin"));
                //e.Row.Cells[3].Text = FormatTime(DataBinder.Eval(e.Row.DataItem, "horaInicio"));
                //e.Row.Cells[4].Text = FormatTime(DataBinder.Eval(e.Row.DataItem, "horaFin"));

                // Las demás columnas las puedes dejar con el valor directo o formatear si es necesario
                //e.Row.Cells[5].Text = DataBinder.Eval(e.Row.DataItem, "direccion").ToString();
                //e.Row.Cells[6].Text = DataBinder.Eval(e.Row.DataItem, "codigoPostal").ToString();
                //var descripcionObj = DataBinder.Eval(e.Row.DataItem, "descripcion");
                //e.Row.Cells[7].Text = (descripcionObj != null && descripcionObj != DBNull.Value) ? descripcionObj.ToString() : "";
                //e.Row.Cells[8].Text = DataBinder.Eval(e.Row.DataItem, "tipoEvento").ToString();
            }
        }


        protected void dgvEventos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEventos.PageIndex = e.NewPageIndex;
            dgvEventos.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarCliente.aspx");
        }

        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            //Empleado empleado = empleados.SingleOrDefault(x => x.IdPersona == idEmpleado);
            // Session["empleadoSeleccionado"] = empleado;
            Response.Redirect("RegistrarCliente.aspx?accion=modificar");
        }

        protected void lbEliminar_Click(object sender, EventArgs e)
        {
            int idEvento = Int32.Parse(((LinkButton)sender).CommandArgument);
            eventoWS.eliminarEvento(idEvento);
            Response.Redirect("ListarClientes.aspx");
        }

        protected void lbVisualizar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            //Empleado empleado = empleados.SingleOrDefault(x => x.IdPersona == idEmpleado);
            //Session["empleadoSeleccionado"] = empleado;
            Response.Redirect("RegistrarCliente.aspx?accion=ver");
        }

        protected void lbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto del textbox
                string textoId = txtNombre.Text.Trim();

                if (int.TryParse(textoId, out int idEvento))
                {
                    // Buscar cliente por ID usando tu capa de negocio o servicio
                    var evento = eventoWS.obtenerEvento(idEvento);

                    if (evento != null)
                    {
                        // Si encontró el cliente, lo pone en una lista para enlazar
                        var lista = new List<evento > { evento };
                        dgvEventos.DataSource = lista;
                        dgvEventos.DataBind();
                        //lblMensaje.Text = "";
                    }
                    else
                    {
                        // Si no encontró resultados
                        dgvEventos.DataSource = null;
                        dgvEventos.DataBind();
                        // lblMensaje.Text = "No se encontró cliente con ese ID.";
                    }
                }
                else
                {
                    // Si no ingresó un número válido
                    dgvEventos.DataSource = null;
                    dgvEventos.DataBind();
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
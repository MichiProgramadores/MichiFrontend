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
        protected List<evento> eventos;
        protected void Page_Init(object sender, EventArgs e)
        {
            eventoWS = new EventoWSClient();
            CargarDatos();
        }
        protected void CargarDatos()
        {
            eventos = eventoWS.listarEventos().ToList();
            dgvEventos.DataSource = eventos;
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
                e.Row.Cells[0].Text = DataBinder.Eval(e.Row.DataItem, "evento_id").ToString();
                e.Row.Cells[1].Text = FormatDate(DataBinder.Eval(e.Row.DataItem, "fechaInicio"));
                e.Row.Cells[2].Text = FormatDate(DataBinder.Eval(e.Row.DataItem, "fechaFin"));
          

                //Las demás columnas las puedes dejar con el valor directo o formatear si es necesario
               
                var descripcionObj = DataBinder.Eval(e.Row.DataItem, "descripcion");
                e.Row.Cells[3].Text = (descripcionObj != null && descripcionObj != DBNull.Value) ? descripcionObj.ToString() : "";
                e.Row.Cells[4].Text = DataBinder.Eval(e.Row.DataItem, "tipoEvento").ToString();
            }
        }


        protected void dgvEventos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEventos.PageIndex = e.NewPageIndex;
            dgvEventos.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarEvento.aspx");
        }

        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idEvento = Int32.Parse(((LinkButton)sender).CommandArgument);
            evento evento= eventos.SingleOrDefault(x => x.evento_id == idEvento);
            Session["eventoSeleccionado"] = evento;
            Response.Redirect("RegistrarEvento.aspx?accion=modificar");
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idEvento = int.Parse(hfIdEliminar.Value);
            eventoWS.eliminarEvento(idEvento);
            Response.Redirect("ListarEventos.aspx");
        }

        protected void lbVisualizar_Click(object sender, EventArgs e)
        {
            int idEvento = Int32.Parse(((LinkButton)sender).CommandArgument);
            evento evento = eventos.SingleOrDefault(x => x.evento_id == idEvento);
            Session["eventoSeleccionado"] = evento;
            Response.Redirect("RegistrarEvento.aspx?accion=ver");
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
                        // lblMensaje.Text = "No se encontró evento con ese ID.";
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
                // lblMensaje.Text = "Error al buscar evento: " + ex.Message;
            }
        }

    }
}
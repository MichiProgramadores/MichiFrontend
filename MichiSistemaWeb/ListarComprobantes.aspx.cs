using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class ListarComprobantes : System.Web.UI.Page
    {
        protected ComprobanteWSClient comprobanteWS;
        protected List<comprobante> comprobantes;
        protected void Page_Load(object sender, EventArgs e)
        {
            comprobanteWS = new ComprobanteWSClient();
            CargarDatos();
        }
        protected void CargarDatos()
        {
            //comprobantes = comprobanteWS.listarComprobante().ToList();
            dgvComprobantes.DataSource = comprobanteWS.listarComprobante();
            dgvComprobantes.DataBind();
        }
        private string FormatDateTime(object obj, string format = "dd/MM/yyyy HH:mm:ss")
        {
            if (obj == null || obj == DBNull.Value) return "";
            string str = obj.ToString();
            if (DateTime.TryParse(str, out DateTime dt))
                return dt.ToString(format);
            return str; // O "" si prefieres ocultar valores no parseables
        }
        protected void dgvComprobantes_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void dgvComprobantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvComprobantes.PageIndex = e.NewPageIndex;
            dgvComprobantes.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarComprobante.aspx");
        }

        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            Response.Redirect("RegistrarComprobante.aspx?accion=modificar");
        }

        protected void lbEliminar_Click(object sender, EventArgs e)
        {
            int idComprobante = Int32.Parse(((LinkButton)sender).CommandArgument);
            comprobanteWS.eliminarComprobante(idComprobante);
            Response.Redirect("ListarComprobantes.aspx");
        }

        protected void lbVisualizar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            Response.Redirect("RegistrarComprobante.aspx?accion=ver");
        }

        protected void lbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto del textbox
                string textoId = txtNombre.Text.Trim();

                if (int.TryParse(textoId, out int idComprobante))
                {
                    // Buscar comprobante por ID usando tu capa de negocio o servicio
                    var comprobante = comprobanteWS.obtenerComprobante(idComprobante);

                    if (comprobante != null)
                    {
                        // Si encontró el comprobante, lo pone en una lista para enlazar
                        var lista = new List<comprobante> { comprobante };
                        dgvComprobantes.DataSource = lista;
                        dgvComprobantes.DataBind();
                        //lblMensaje.Text = "";
                    }
                    else
                    {
                        // Si no encontró resultados
                        dgvComprobantes.DataSource = null;
                        dgvComprobantes.DataBind();
                        // lblMensaje.Text = "No se encontró comprobante con ese ID.";
                    }
                }
                else
                {
                    // Si no ingresó un número válido
                    dgvComprobantes.DataSource = null;
                    dgvComprobantes.DataBind();
                    //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                }
            }
            catch (Exception ex)
            {
                // lblMensaje.Text = "Error al buscar comprobante: " + ex.Message;
            }
        }

    }
}
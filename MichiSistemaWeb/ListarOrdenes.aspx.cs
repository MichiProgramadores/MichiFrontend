using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class ListarOrdenes : System.Web.UI.Page
    {

        protected OrdenWSClient ordenWS;
        protected List<orden> ordenes;
        protected void Page_Init(object sender, EventArgs e)
        {
            ordenWS = new OrdenWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarDatos();

        }
        protected void CargarDatos()
        {
            try
            {
                ordenes = ordenWS.listarOrdenes().ToList();
                dgvOrdenes.DataSource = ordenes;
                dgvOrdenes.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al cargar las ordenes: " + ex.Message;
            }
        }


        private string FormatDateTime(object obj, string format = "dd/MM/yyyy HH:mm:ss")
        {
            if (obj == null || obj == DBNull.Value) return "";
            string str = obj.ToString();
            if (DateTime.TryParse(str, out DateTime dt))
                return dt.ToString(format);
            return str; // O "" si prefieres ocultar valores no parseables
        }
        protected void GvVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvOrdenes.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void BtnNuevaOrden_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarOrden.aspx");
        }

        protected void dgvOrdenes_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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
            int idOrden = Int32.Parse(((LinkButton)sender).CommandArgument);
            orden orden = ordenes.SingleOrDefault(x => x.idOrden == idOrden);
            if (orden != null)
            {
                Session["ordenSeleccionada"] = orden;
            }
            // 
            Response.Redirect("RegistrarOrden.aspx?accion=modificar");
        }

        /*
        protected void lbEliminar_Click(object sender, EventArgs e)
        {
            int idOrden = Int32.Parse(((LinkButton)sender).CommandArgument);
            ordenWS.eliminarOrden(idOrden);
            Response.Redirect("ListarOrdenes.aspx");
        }
        */

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idOrden = int.Parse(hfIdEliminar.Value);
            ordenWS.eliminarOrden(idOrden);
            Response.Redirect("ListarOrdenes.aspx");
        }

        protected void lbVerDetalles_Click(object sender, EventArgs e)
        {
            //int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);


            try
            {
                LinkButton btn = (LinkButton)sender;
                int idOrden = Convert.ToInt32(btn.CommandArgument);

                orden orden = ordenWS.obtenerOrden(idOrden);
                if (orden != null)
                {
                    Session["ordenSeleccionada"] = orden;
                }
                dgvDetalles.DataSource = orden.listaOrdenes;
                dgvDetalles.DataBind();

                string script = "window.onload = function() {showModalForm(); };";
                ClientScript.RegisterStartupScript(GetType(), "", script, true);
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al cargar los detalles: " + ex.Message;
            }
            Response.Redirect("RegistrarOrden.aspx?accion=ver");
        }

        protected void lbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto del textbox
                string textoId = txtNombreID.Text.Trim();

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
                    dgvOrdenes.DataSource = ordenes;
                    dgvOrdenes.DataBind();
                    //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                }
            }
            catch (Exception ex)
            {
                // lblMensaje.Text = "Error al buscar cliente: " + ex.Message;
            }
            txtNombreID.Text = "";
        }
    }
}

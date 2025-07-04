﻿using MichiSistemaWeb.MichiBackend;
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
            txtNombreID.Text = "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //var masterPage = (MichiSistemaWeb.Michi)Master;
            //var lblMensajeBusqueda = masterPage.FindControl("lblMensajeBusqueda") as Label;
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
                //lblError.Text = "Error al cargar las ordenes: " + ex.Message;
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
                string tipoSeleccionado = DdlTipoRecepcion.SelectedValue;

                List<orden> listaCompleta = ordenWS.listarOrdenes().ToList();
                List<orden> resultados = listaCompleta;
                if(!string.IsNullOrEmpty(textoId) && (tipoSeleccionado != "0" && !string.IsNullOrEmpty(tipoSeleccionado)))
                {
                    resultados = resultados
                    .Where(c => c.idOrden.ToString().Contains(textoId) && c.tipoRecepcion.ToString() == tipoSeleccionado)
                    .ToList();
                }

                if (!string.IsNullOrEmpty(textoId) && (tipoSeleccionado == "0" || string.IsNullOrEmpty(tipoSeleccionado)))
                {
                    resultados = resultados
                        .Where(c => c.idOrden.ToString().Contains(textoId))
                        .ToList();
                }
                if (tipoSeleccionado != "0" && !string.IsNullOrEmpty(tipoSeleccionado) && string.IsNullOrEmpty(textoId))
                {
                    resultados = resultados
                        .Where(c => c.tipoRecepcion.ToString().ToUpper().Contains(tipoSeleccionado.ToUpper()))
                        .ToList();
                }
                
                if (textoId == "" && tipoSeleccionado == "0")
                {
                    resultados = ordenWS.listarOrdenes().ToList();
                }
                   dgvOrdenes.DataSource = resultados;
                   dgvOrdenes.DataBind();

                    // 5. Opcional: Guardar en ViewState
                   ViewState["OrdenesFiltradas"] = resultados;
            }
            catch (Exception ex)
            {
                dgvOrdenes.DataSource = null;
                dgvOrdenes.DataBind();
                // lblMensaje.Text = "Error: " + ex.Message;
            }

        }
    }
}

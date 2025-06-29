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
    public partial class ListarComprobantes : System.Web.UI.Page
    {
        protected ComprobanteWSClient comprobanteWS;
        protected List<comprobante> comprobantes;

        protected void Page_Init(object sender, EventArgs e)
        {
            comprobanteWS = new ComprobanteWSClient();
            //CargarDatos();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void ListarTodos_Click(object sender, EventArgs e)
        {
            comprobantes = comprobanteWS.listarComprobante().ToList();
            ViewState["ComprobantesFiltrados"] = comprobantes; // Guarda en ViewState
            dgvComprobantes.DataSource = comprobantes;
            dgvComprobantes.DataBind();
            txtNombre.Text = "";
        }

        protected void CargarDatos()
        {
            try
            {
                // Si ya está cargado en ViewState (evita llamar al WS de nuevo)
                if (ViewState["ComprobantesFiltrados"] != null)
                {
                    comprobantes = (List<comprobante>)ViewState["ComprobantesFiltrados"];
                }
                else
                {
                    // Si no, filtra y guarda en ViewState
                    comprobantes = comprobanteWS.listarComprobante()
                                          .Where(c => c.estado != "ELIMINADO")
                                          .ToList();
                    ViewState["ComprobantesFiltrados"] = comprobantes;
                }

                dgvComprobantes.DataSource = comprobantes;
                dgvComprobantes.DataBind();
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void dgvComprobantes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //
        }

        protected void dgvComprobantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvComprobantes.PageIndex = e.NewPageIndex; // Cambia la página
            CargarDatos(); // Vuelve a cargar los datos (ya filtrados y con ViewState)
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarComprobante.aspx");
        }

        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idComprobante = Int32.Parse(((LinkButton)sender).CommandArgument);

            comprobante comprobante = comprobantes.SingleOrDefault(x => x.id_comprobante == idComprobante);

            if (comprobante != null)
            {
                // Almacenar el cliente seleccionado en la sesión
                Session["comprobanteSeleccionado"] = comprobante;

                // Redirigir a la página de edición:
                Response.Redirect("RegistrarComprobante.aspx?accion=modificar");
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idComprobante = int.Parse(hfIdEliminar.Value);
            comprobanteWS.eliminarComprobante(idComprobante);
            //comprobanteWS.actualizarEstadoComprobante(idComprobante, "ELIMINADO");
            Response.Redirect("ListarComprobantes.aspx");
        }

        protected void lbVisualizar_Click(object sender, EventArgs e)
        {
            int idComprobante = Int32.Parse(((LinkButton)sender).CommandArgument);
            comprobante comprobante = comprobantes.SingleOrDefault(x => x.id_comprobante == idComprobante);
            Session["comprobanteSeleccionado"] = comprobante;
            Response.Redirect("RegistrarComprobante.aspx?accion=ver");
        }
        
        protected void lbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto del textbox
                string textoId = txtNombre.Text.Trim();
                string textoIdOrden = txtIdOrden.Text.Trim();
                List<comprobante> listaCompleta = comprobanteWS.listarComprobante().ToList();
                List<comprobante> resultados = listaCompleta;

                // 3. Aplicar filtros SOLO si el campo tiene valor
                if (textoId != "" && !string.IsNullOrEmpty(textoIdOrden))
                {
                    resultados = resultados
                       .Where(c => c.id_comprobante.ToString() == textoId && c.orden_id.ToString().Contains(textoIdOrden))
                       .ToList();
                }
                else
                {
                    if (!string.IsNullOrEmpty(textoId))
                    {
                        resultados = resultados
                            .Where(c => c.id_comprobante.ToString().Contains(textoId))
                            .ToList();
                    }

                    if (!string.IsNullOrEmpty(textoIdOrden))
                    {
                        resultados = resultados
                            .Where(c => c.orden_id.ToString().Contains(textoIdOrden))
                            .ToList();
                    }
                    if (textoId == "" && textoIdOrden == "")
                    {
                        resultados = comprobanteWS.listarComprobante().ToList();
                    }
                }

                // 4. Mostrar resultados
                dgvComprobantes.DataSource = resultados;
                dgvComprobantes.DataBind();

                // 5. Opcional: Guardar en ViewState
                //if (int.TryParse(textoId, out int idComprobante))
                //{
                //    // Buscar comprobante por ID usando tu capa de negocio o servicio
                //    var comprobante = comprobanteWS.obtenerComprobante(idComprobante);

                //    if (comprobante != null)
                //    {
                //        // Si encontró el comprobante, lo pone en una lista para enlazar
                //        var lista = new List<comprobante> { comprobante };
                //        dgvComprobantes.DataSource = lista;
                //        dgvComprobantes.DataBind();
                //        //lblMensaje.Text = "";
                //    }
                //    else
                //    {
                //        // Si no encontró resultados
                //        dgvComprobantes.DataSource = null;
                //        dgvComprobantes.DataBind();
                //        // lblMensaje.Text = "No se encontró comprobante con ese ID.";
                //    }
                //}
                //else
                //{
                //    if (txtNombre.Text == "")
                //    {
                //        // Si no ingresó un número válido
                //        dgvComprobantes.DataSource = comprobantes;
                //        dgvComprobantes.DataBind();
                //        //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                //    }
                //    else
                //    {
                //        // Si no ingresó un número válido
                //        dgvComprobantes.DataSource = null;
                //        dgvComprobantes.DataBind();
                //        //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                //    }
                //}
                ViewState["TrabajadoresFiltrados"] = resultados;
            }
            catch (Exception ex)
            {
                // lblMensaje.Text = "Error al buscar comprobante: " + ex.Message;
                dgvComprobantes.DataSource = null;
                dgvComprobantes.DataBind();
            }
        }
        protected void lbBuscarOrden_Click(object sender, EventArgs e)
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
                    if (txtNombre.Text == "")
                    {
                        // Si no ingresó un número válido
                        dgvComprobantes.DataSource = comprobantes;
                        dgvComprobantes.DataBind();
                        //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                    }
                    else
                    {
                        // Si no ingresó un número válido
                        dgvComprobantes.DataSource = null;
                        dgvComprobantes.DataBind();
                        //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                    }
                }
            }
            catch (Exception ex)
            {
                // lblMensaje.Text = "Error al buscar comprobante: " + ex.Message;
                dgvComprobantes.DataSource = null;
                dgvComprobantes.DataBind();
            }
        }


    }
}